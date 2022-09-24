using OpenCvSharp;

namespace ImageLib{
    public partial class ImageData{
        /// <summary>
        /// 显示一个标题为"name"图像窗口
        /// </summary>
        /// <param name="name">窗口标题</param>
        public void ShowSelf(string name){
            if (!ImageMat.Empty()) Cv2.ImShow(name,ImageMat);
        }
        /// <summary>
        /// 获取图像的文件路径
        /// </summary>
        /// <returns>文件路径</returns>
        public string? GetPath(){
            return path;
        }
        /// <summary>
        /// 获取图像所有通道上的混合矩阵
        /// </summary>
        /// <returns>总矩阵</returns>
        public Mat GetMat(){
            return ImageMat;
        }
        /// <summary>
        /// 获取特定通道的图像矩阵
        /// </summary>
        /// <param name="channel">指定的图像颜色通道</param>
        /// <returns>指定的通道矩阵</returns>
        public Mat GetMatOfChannel(int channel){
            if (channel >= ImageMat.Channels()){
                throw new Exception("The channel out of channels limit");
            }
            return ImageMat.ExtractChannel(channel);
        }
        public void SetMat(Mat matrix){
            ImageMat = matrix;
        }
        /// <summary>
        /// 等待下一个按键按下
        /// </summary>
        public static void WaitForKey(){
            Cv2.WaitKey();
        }
        /// <summary>
        /// 结束所有OpenCv窗口
        /// </summary>
        public static void DestroyWindows(){
            Cv2.DestroyAllWindows();
        }
    }
}