using ImageFilter;

namespace Editor{
    public partial class ImageEditor{
        public void Edit(){
            switch (EditorType){
                case ImageEditType.Mixing:
                    Mixing();
                    break;
                case ImageEditType.Filter:
                    Filter();
                    break;
                case ImageEditType.EdgeDetect:
                    EdgeDetect();
                    break;
            }
        }
        #nullable disable
        private void Filter(){
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Input the Operator Size, none for size 1:");
            var s = Console.ReadLine();
            var OptSize = int.Parse((s.Length>=4||s=="")?"1":s);


            Console.WriteLine("Input the Operator Type, none for primitive, other for Optimize:");
            var OptType = (Console.ReadLine()!="")?FilterOperatorTypeCode.Primitive:FilterOperatorTypeCode.Optimize;
            Console.WriteLine("----------------------------------------");


            Console.WriteLine("----------------------------------------");
            Console.WriteLine("*************Filter Type:***************");
            Console.WriteLine("************0: Median Filter************");
            Console.WriteLine("*************1: Mean Filter*************");
            Console.WriteLine("***********2: Gaussian Filter***********");
            Console.WriteLine("Input The Filter Type, none for MedianFilter:");
            s = Console.ReadLine();
            var FilterTypeChoose = int.Parse((s.Length>=2||s=="")?"1":s);
            FilterTypeCode fltType = FilterTypeCode.Median;
            switch (FilterTypeChoose){
                case 0:
                    fltType = FilterTypeCode.Median;
                    break;
                case 1:
                    fltType = FilterTypeCode.Mean;
                    break;
                case 2:
                    fltType = FilterTypeCode.Gaussian;
                    break;
                default:
                    break;
            }
            Console.WriteLine("----------------------------------------");


            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Filter Process Start.");
            Filters filter = new(fltType,OptType,OptSize);
            //Mat dst = new();
            //Cv2.MedianBlur(Images["Source"].GetMat(),dst,3);
            //Images["Output"].SetMat(dst);
            //Images["Source"].SetMat(srcMat);
            Images["Output"].SetMat(filter.ApplyFilter(Images["Source"].GetMat()));
            Console.WriteLine("Filter Process Finish.");
            Console.WriteLine("----------------------------------------");
        }
        private void EdgeDetect(){

        }
        private void Mixing(){

        }
        private void SeamLessCloning(){

        }
    } 
}