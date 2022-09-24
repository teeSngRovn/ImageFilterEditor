using OpenCvSharp;
//#define __debug__

namespace ImageFilter{
    public enum FilterOperatorTypeCode{
        Primitive,
        Optimize,
    }
    public enum FilterTypeCode{
        Gaussian,
        Mean,
        Median,

    }
    public class Filters{
        private IFilter Filter;
        private int OperatorSize;
        private FilterOperatorTypeCode OperatorType;
        public Filters(FilterTypeCode typeCode,FilterOperatorTypeCode OptType,int OptSize = 1){
            switch (typeCode){
                case FilterTypeCode.Gaussian:
                    Filter = new GaussianFilter();
                    break;
                case FilterTypeCode.Mean:
                    Filter = new MeanFilter();
                    break;
                case FilterTypeCode.Median:
                    Filter = new MedianFilter();
                    break;
                default:
                    Filter = new MeanFilter();
                    break;
            }
            OperatorSize = OptSize;
            OperatorType = OptType;
        }
        public Mat ApplyFilter(Mat srcMat){
            return Filter.ApplyFilter(srcMat,OperatorSize,OperatorType);
        }
        public void SetSize(int size){
            OperatorSize = Math.Min(Math.Max(size,0),255);
        }
    }
}