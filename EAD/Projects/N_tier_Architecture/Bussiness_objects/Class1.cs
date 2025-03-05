namespace Bussiness_objects
{
	public class Emp
	{
		private int age;
		private int salary;

		public int Age {

			get { 
				
				return age;
			}

			set
			{
				if (value > 10)
				{

					age = value;
				}
			}
		
		
		}

		public int Salary {

			get
			{
				return salary;
			}
			set
			{

				salary = value;
			}
		
		}
	}
}
