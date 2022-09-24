using OpenCvSharp;

namespace ImageLib{
    public partial class ImageData{
        private Mat ImageMat;
        private string? path;
        public ImageData(bool readFile = true){
            if (readFile){
                ImageMat = ReadImage();
            }else{
                ImageMat = new();
            }
        }
        #nullable disable
        private Mat ReadImage(){
            ReadPathString();
            Mat mat = ReadPathFile(path);
            return mat;
        }
        private void ReadPathString(){
            Console.WriteLine("Input the file path:");
            path = Console.ReadLine();
            while (!File.Exists(path)){
                Console.WriteLine("Path point to unexist file, input the file path again:");
                path = Console.ReadLine();
            }
        }
        private Mat ReadPathFile(string path,ImreadModes Mode = ImreadModes.Color){
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Read Start.");
            var result = Cv2.ImRead(path,Mode);
            Console.WriteLine("Read Finished.");
            Console.WriteLine("----------------------------------------");
            return result;
        }
    }   
}