namespace semath
{
    public class math_se
    {
     

        public int sum(params int[] ary)
        {
            int sum = 0;
            foreach (int item in ary)
            {
                sum = sum + item;
            }
            return sum;
        }
    }

}
