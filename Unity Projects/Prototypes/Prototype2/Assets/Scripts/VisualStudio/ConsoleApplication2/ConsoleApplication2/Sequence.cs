using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

//namespace ConsoleApplication2
//{
class Sequence : Diagram
{
    public ArrayList Lifelines { get; set; }

    public Sequence()
    {
        this.Lifelines = new ArrayList();
        //this.InteractionOperator = new ArrayList();
    }

    public void addLifeline(Lifeline lifeline)
    {
        this.Lifelines.Add(lifeline);
    }

    public void orderLifeline()
    {
        ArrayList Lifelines2 = (ArrayList)this.Lifelines.Clone();
        Lifelines.Clear();
        List<int> vector = new List<int>();
        int size = Lifelines2.Count;
        int aux;

        foreach (Lifeline l in Lifelines2)
        {
            vector.Add(l.Seqno);
        }

        for (int i = size - 1; i >= 1; i--)
        {
            for (int j = 0; j < i; j++)
            {
                if (vector[j] < vector[j + 1])
                {
                    aux = vector[j];
                    vector[j] = vector[j + 1];
                    vector[j + 1] = aux;
                }
            }
        }

        foreach (int v in vector)
        {
            foreach (Lifeline l in Lifelines2)
            {
                if (l.Seqno == v)
                {
                    this.Lifelines.Add(l);
                }
            }
        }

    }

    public void orderMessage()
    {
        foreach (Lifeline l in this.Lifelines)
        {
            List<Message> Messages2 = new List<Message>();
            List<int> vector = new List<int>();
            int size = l.Messages.Count;
            int aux;

            foreach (Message m in l.Messages)
            {
                //Console.WriteLine(m.Seqno + " " +m.Id);
                vector.Add(m.Seqno);
                Messages2.Add(m);
            }

            for (int i = size - 1; i >= 1; i--)
            {
                for (int j = 0; j < i; j++)
                {
                    if (vector[j] > vector[j + 1])
                    {
                        aux = vector[j];
                        vector[j] = vector[j + 1];
                        vector[j + 1] = aux;
                    }
                }
            }

            l.Messages.Clear();

            foreach (int v in vector)
            {
                foreach (Message m in Messages2)
                {
                    if (v == m.Seqno)
                    {
                        l.Messages.Add(m);
                    }
                }
            }
        }
    }
}
//}