using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class RetryButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Tooltip("���̃{�^���̉摜")] Image _image;
    [Tooltip("�t�F�[�h�C����̐F")] Color _imageColor = new(1, 1, 1, 1);
    [SerializeField, Header("�X�g�[���[�e�L�X�g�}�l�[�W���[")] StoryTextManager _storyTextManager;
    [SerializeField, Header("�X�g�[���[�e�L�X�g")] Text _storyText;

    private void Start()
    {
        _image = GetComponent<Image>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {

        if(_storyTextManager.DiscoverRetry)
        {
            _image.color = _imageColor - new Color(100 / 255f, 100 / 255f, 100 / 255f, 0);
            _storyText.DOFade(0, 2.0f).SetEase(Ease.Linear).SetAutoKill();
            _image.DOFade(0, 2.0f).SetEase(Ease.Linear).OnComplete(() => SceneManager.LoadScene("Game")).SetAutoKill();
        }
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        if(_storyTextManager.DiscoverRetry)
        {
            _image.color = _imageColor - new Color(55 / 255f, 55 / 255f, 55 / 255f, 0);
        }
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {

        if(_storyTextManager.DiscoverRetry)
        {
            _image.color = _imageColor;
        }
        
    }

}
