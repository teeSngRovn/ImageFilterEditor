using ImageLib;

namespace Editor{
    public partial class ImageEditor{
        public void ShowImages(){
            Console.WriteLine("Start Showing Images.");
            foreach(var i in Images){
                i.Value.ShowSelf(i.Key);
            }
            ImageData.WaitForKey();
            Console.WriteLine("Finish Showing Images.");
        }
        public void Exit(){
            Console.WriteLine("Start Destroy Windows.");
            ImageData.DestroyWindows();
            Console.WriteLine("Finish Destroy Windows.");
        }
    }
}