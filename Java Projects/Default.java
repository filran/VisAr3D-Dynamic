public class Default
{
	public static void main(String args[])
	{
		Student filipe = new Student();
		filipe.name = "filipe";
		filipe.email = "filran@gmail.com";
		filipe.graduate = "Science Computer";

		Professor susie = new Professor();
		susie.name = "Susie";
		susie.email = "susie@gmail.com";
		susie.phd = "Software Engineering";

		System.out.println(filipe.name);
	}
}