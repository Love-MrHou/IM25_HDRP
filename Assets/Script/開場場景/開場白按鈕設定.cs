using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoButtonController : MonoBehaviour
{
    public Button myButton;        // 按钮对象
    public VideoPlayer videoPlayer; // 视频播放器对象

    void Start()
    {
        // 开始时隐藏按钮
        myButton.gameObject.SetActive(false);

        // 监听视频播放结束事件
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    // 当视频播放结束时调用
    void OnVideoEnd(VideoPlayer vp)
    {
        // 显示按钮
        myButton.gameObject.SetActive(true);
    }
}
