public abstract class MenuComponent
{
	//methods to composiste
	public void add (MenuComponent menuComponent)
	{
		throw new UnsupportedOperationException();
	}

	public void remove (MenuComponent menuComponent)
	{
		throw new UnsupportedOperationException();
	}


	//methods to items
	public MenuComponent getChild (int i)
	{
		throw new UnsupportedOperationException();
	}

	public String getName()
	{
		throw new UnsupportedOperationException();	
	}

	public String getDescription()
	{
		throw new UnsupportedOperationException();	
	}

	public double getPrice()
	{
		throw new UnsupportedOperationException();	
	}

	public boolean isVegeterian()
	{
		throw new UnsupportedOperationException();	
	}


	//method to item and composite
	public void print()
	{
		throw new UnsupportedOperationException();	
	}

}