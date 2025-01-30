using Business_Objects;
using Business_Logic_Layer;
using Data_Access_Layer;
using Presentation_Layer;

namespace N_Tier_Architecture{
    public class Program
    {
        public static void Main(string[] args)
        {
            Emp emp = new Emp();
            PL pl = new PL();
            BL bl = new BL();
            DAL dal = new DAL();

            emp = pl.getInput(emp);
            emp = bl.calculateSalary(emp);
            if(dal.save(emp)==true)
            {
                Console.WriteLine("Data Saved");
            }
            else
            {
                Console.WriteLine("Data Not Saved");
            }

            pl.display(emp);
        }
    }
}