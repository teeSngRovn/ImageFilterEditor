using ImageLib;

namespace Editor{
    public enum ImageEditType{
            Mixing,
            Filter,
            EdgeDetect,
    }
    public partial class ImageEditor{
        private Dictionary<string,ImageData> Images;
        ImageEditType EditorType;
        public ImageEditor(ImageEditType EditType){
            Images = new();
            EditorType = EditType;  
            switch (EditorType){
                case ImageEditType.Mixing:
                    MixingInit();
                    break;
                case ImageEditType.Filter:
                    FilterInit();
                    break;
                case ImageEditType.EdgeDetect:
                    EdgeDetectInit();
                    break;
            }
        }
        private void MixingInit(){
            Images.Add("SourceForeground",new ImageData());
            Images.Add("SourceBackground",new ImageData());
            Images.Add("Output",new ImageData(readFile:false));
        }
        private void FilterInit(){
            Images.Add("Source",new ImageData());
            Images.Add("Output",new ImageData(readFile:false));
        }
        private void EdgeDetectInit(){
            Images.Add("Source",new ImageData());
            Images.Add("Output",new ImageData(readFile:false));
        }
    }
}