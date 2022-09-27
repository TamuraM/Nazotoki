using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;

/// <summary>ゲームマネージャー！！！！！！！！！</summary>
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField, Header("制限時間")] float _timeLimit = 600f;
    [Tooltip("制限時間表示用 1秒カウントさせる")] float _second = 1.0f;
    [Tooltip("制限時間(分)")] int _limitMinute = 10;
    [Tooltip("制限時間(分)")] int _limitSecond = 0;
    [SerializeField, Header("制限時間を表示するテキスト")] Text _time;

    //ドアの横にあるライト
    [SerializeField, Header("ドアのライトのゲームオブジェクト")] MeshRenderer[] _doorLights;
    [SerializeField, Header("光ってないマテリアル"), Tooltip("ライトがひかってないときのマテリアル")] Material _lightMaterial;
    [SerializeField, Header("光るMaterial"), Tooltip("ライトが光ってるマテリアル")] Material _lightEmissionMaterial;

    //左側の謎関係
    [SerializeField, Header("左側の謎の背景"), Tooltip("左側の謎解きがかいてある画像")] GameObject _leftNazoBackground;
    [SerializeField, Header("入力するテキストGameObject"), Tooltip("タイプライターをクリックしたときに出てくる入力画面パネル")] GameObject _inputText;
    [SerializeField, Header("PushMeボタン"), Tooltip("PushMeボタンのゲームオブジェクト")] GameObject _pushMeButton;

    //後ろ側の謎関係
    [Tooltip("背面にある色付きボタンのリスト ↓の配列を入れる")] List<GameObject> _colorButtonAnswer = new();
    [SerializeField, Tooltip("ボタンの配列 押す順番に入ってる")] GameObject[] _colorButtonAnswerArray; //黄、白、青、赤、緑
    [SerializeField, Tooltip("ボタンを正しく押せた時に光るライトのリスト")] List<MeshRenderer> _colorButtonLights = new(5);
    [Tooltip("ライトのやつカウントする数字")] int _lighting = 0;
    [SerializeField, Header("クリア時のライトのマテリアル")] Material _clearLightMaterial;

    //右側の謎関係
    [SerializeField, Header("モニターの画像")] SpriteRenderer _monitor;
    [SerializeField, Header("謎の画像たちリスト")] Sprite[] _questions = new Sprite[2];
    [SerializeField, Header("ボタンを押す順番に入れる")] GameObject[] _monitorButtonAnswerArray = new GameObject[2];
    [Tooltip("押すボタンのList ↑の配列を入れる")] List<GameObject> _monitorButtonAnswer = new();
    [SerializeField, Header("まるの画像")] Sprite _circle; //正解の時
    [SerializeField, Header("ばつの画像")] Sprite _cross; //不正解の時
    [Tooltip("今何問目か")] int _clearSprite = 0;

    //ドアの謎関係
    [SerializeField, Header("ドアの謎")] GameObject _lastNazo;
    [SerializeField, Header("テンキー")] GameObject _numberKey;
    [SerializeField, Header("ドアの謎の背景")] GameObject _lastNazoBackground;
    [SerializeField, Header("番号入力画面")] GameObject _numberKeyBackground;

    //クリアしたときの音
    [SerializeField, Header("ナンバーキー")] AudioSource _lightAudio;
    bool _clearLeftNazo;
    bool _clearBackNazo;
    bool _clearRightNazo;

    public bool _isFocused;

    /// <summary>ゲーム内の状態</summary>
    public enum GameMode
    {
        Title, //タイトル画面
        Opening, //オープニング中
        PlayGame, //ゲームプレイ中
        Thinking, //ヒントを考えてる
        GameClear, //脱出成功のエンディング
        GameOver, //時間切れのエンディング
        Credit, //クレジット表示
    }

    public GameMode _gameMode;

    /// <summary>謎解きの進行度</summary>
    public enum Clear
    {
        ClearSitenaiMan = 0,
        /// <summary>最初の謎解けた</summary>
        FirstStageClear = 1 << 0,
        /// <summary>二番目の謎解けた</summary>
        SecondStageClear = 1 << 1,
        /// <summary>三番目の謎解けた</summary>
        ThirdStageClear = 1 << 2,
        /// <summary>すべての謎解けて、最後の謎出現</summary>
        LastStageStart = 1 << 3,
        /// <summary>最後の謎解けた</summary>
        LastStageClear = 1 << 4,
    }

    public Clear _clearState;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        _clearState = Clear.ClearSitenaiMan;
        _gameMode = GameMode.Title;

        _pushMeButton.SetActive(false);
        _leftNazoBackground.SetActive(false);
        _inputText.SetActive(false);
        _lastNazo.SetActive(false);
        _numberKey.SetActive(false);
        _lastNazoBackground.SetActive(false);
        _numberKeyBackground.SetActive(false);

        _colorButtonAnswer = _colorButtonAnswerArray.ToList();
        _monitorButtonAnswer = _monitorButtonAnswerArray.ToList();
        _monitor.sprite = _questions[_clearSprite];
    }


    void Update()
    {
        
        if(_gameMode == GameMode.PlayGame || _gameMode == GameMode.Thinking)
        {
            //制限時間減って、０になったらゲームオーバー
            _timeLimit -= Time.deltaTime;

            if(_timeLimit < 0)
            {
                _limitMinute = 0;
                _limitSecond = 0;
                _gameMode = GameMode.GameOver;
            }

            //制限時間表示
            _second -= Time.deltaTime;

            if(_second < 0)
            {

                if(_limitSecond > 0)
                {
                    _limitSecond--;
                }
                else
                {
                    _limitSecond = 59;
                    _limitMinute--;
                }

                _second = 1.0f;
            }

            _time.text = $"{_limitMinute:00}:{_limitSecond:00}";
        }
        

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (_gameMode == GameMode.PlayGame)
        {
            if (Physics.Raycast(ray, out hit, 10.0f, 3) && Input.GetMouseButtonDown(0))
            {
                //Debug.Log(hit.collider.gameObject.name);

                if ((_clearState & Clear.FirstStageClear) != Clear.FirstStageClear && _clearState != Clear.LastStageStart)
                {
                    //----------左側にある謎解き----------
                    //机の上の紙クリックしたら謎が拡大される
                    if (hit.collider.gameObject.name == "Nazo" && !_isFocused)
                    {
                        Debug.Log("なぞだ");
                        _leftNazoBackground.SetActive(true);
                        _isFocused = true; ;
                    }

                    //タイプライターをクリックしたら入力画面が出てくる
                    if (hit.collider.gameObject.name == "Typewriter" && !_isFocused)
                    {
                        Debug.Log("タイプライターだ");
                        _inputText.SetActive(true);
                        _isFocused = true; ;
                    }

                    //ボタンをクリックしたらクリア
                    if (hit.collider.gameObject == _pushMeButton && !_clearLeftNazo)
                    {
                        _clearLeftNazo = true;
                        _lightAudio.Play();
                        _doorLights[0].material = _lightEmissionMaterial;
                        _clearState |= Clear.FirstStageClear;
                    }
                    //----------ここまで----------
                }

                if ((_clearState & Clear.SecondStageClear) != Clear.SecondStageClear && _clearState != Clear.LastStageStart)
                {
                    //----------背面にある謎解き----------　クリックした順番があってたらクリア
                    if (hit.collider.gameObject == _colorButtonAnswer[0])
                    {
                        //正解のボタンを押したら、上にあるライトが順番に点く
                        _colorButtonAnswer.RemoveAt(0);
                        _colorButtonLights[_lighting].material = _lightEmissionMaterial;

                        if (_lighting < 5)
                        {
                            _lighting++;
                        }

                    }
                    else if (hit.collider.gameObject != _colorButtonAnswer[0] && hit.collider.gameObject.tag == "ColorButton")
                    {
                        //間違ったボタンを押したら、上にあるライトが全部消えて、最初からになる
                        _colorButtonAnswer = _colorButtonAnswerArray.ToList();
                        Debug.Log(_colorButtonAnswer.Count);
                        _lighting = 0;
                        _colorButtonLights.ForEach(light => light.material = _lightMaterial);
                    }

                    //クリアしたら、2個目のライトを光らせる
                    if (_colorButtonAnswer.Count == 0 && !_clearBackNazo)
                    {
                        _clearBackNazo = true;
                        _lightAudio.Play();
                        _colorButtonLights.ForEach(light => light.material = _clearLightMaterial);
                        _doorLights[1].material = _lightEmissionMaterial;
                        _clearState |= Clear.SecondStageClear;
                    }
                    //----------ここまで----------
                }

                if ((_clearState & Clear.ThirdStageClear) != Clear.ThirdStageClear && _clearState != Clear.LastStageStart)
                {
                    //----------右側にある謎解き----------　画像に対応したボタンを順番に押せたらクリア
                    if (hit.collider.gameObject == _monitorButtonAnswer[0])
                    {
                        //正解したらまるの画像がでてきて、次の問題が表示される
                        _clearSprite++;
                        _monitorButtonAnswer.RemoveAt(0);
                        StartCoroutine(SpriteQuestionSucces());
                    }
                    else if(hit.collider.gameObject != _monitorButtonAnswer[0] && hit.collider.gameObject.tag == "SpriteButton")
                    {
                        _clearSprite = 0;
                        _monitorButtonAnswer = _monitorButtonAnswerArray.ToList();
                        StartCoroutine(SpriteQuestionWrong());
                    }

                    if (_monitorButtonAnswer.Count == 0 && !_clearRightNazo)
                    {
                        _clearRightNazo = true;
                        _lightAudio.Play();
                        _monitor.sprite = _circle;
                        _monitorButtonAnswer.Add(this.gameObject);
                        _doorLights[2].material = _lightEmissionMaterial;
                        _clearState |= Clear.ThirdStageClear;
                    }
                    //----------ここまで----------
                }


                //全部の謎解いたら、最後の謎が出現
                if ((_clearState & Clear.FirstStageClear) == Clear.FirstStageClear && (_clearState & Clear.SecondStageClear) == Clear.SecondStageClear && (_clearState & Clear.ThirdStageClear) == Clear.ThirdStageClear)
                {
                    _clearState = Clear.LastStageStart;
                }


                if ((_clearState & Clear.LastStageClear) != Clear.LastStageClear)
                {

                    //----------ドアにある謎解き----------　全問正解したら出てくる　答えの番号を打ち込めばクリア
                    if (_clearState == Clear.LastStageStart)
                    {
                        _doorLights.ToList().ForEach(m => m.material = _lightEmissionMaterial);
                        _lastNazo.SetActive(true);
                        _numberKey.SetActive(true);
                    }

                    if (hit.collider.gameObject == _lastNazo && !_isFocused)
                    {
                        _lastNazoBackground.SetActive(true);
                        _isFocused = true;
                    }

                    if (hit.collider.gameObject == _numberKey && !_isFocused)
                    {
                        _numberKeyBackground.SetActive(true);
                        _isFocused = true;
                    }
                    //----------ここまで----------
                    
                }

            }

        }



    }

    IEnumerator SpriteQuestionSucces()
    {
        _monitor.sprite = _circle;
        yield return new WaitForSeconds(0.5f);
        _monitor.sprite = _questions[_clearSprite];
    }

    IEnumerator SpriteQuestionWrong()
    {
        _monitor.sprite = _cross;
        yield return new WaitForSeconds(0.5f);
        _monitor.sprite = _questions[_clearSprite];
    }
}
