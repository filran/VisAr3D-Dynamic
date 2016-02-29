import java.util.*;

public class Pai
{
	String name;
	List<Filho> filhos = new ArrayList<Filho>();

	// Pai(String s)
	// {
	// 	this.name = s;
	// }

	public void setName(String s)
	{
		name = s;
	}

	public String getName()
	{
		return name;
	}

	public void add(Filho f)
	{
		filhos.add(f);
	}
}