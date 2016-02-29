public class Test
{
	public static void main(String[] args) {
		Pai pai = new Pai();
		pai.setName("PAI");

		Pai filho = new Filho("FILHO");
		System.out.println(filho.getName());
	}
}