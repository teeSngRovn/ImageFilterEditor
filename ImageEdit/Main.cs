using Editor;
public class MainClass{
    static void Main(){
        Editor.ImageEditor editor = new(ImageEditType.Filter);
        editor.Edit();
        editor.ShowImages();
    }
}