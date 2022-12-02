using System;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

StepGenerator test = new StepGenerator(3, "TEST", 1, 3);
RandomGenerator test1 = new RandomGenerator(3, 1, 10, "TEST");


Console.WriteLine(test1.GenerateNumber());
Console.WriteLine(test1.GenerateNumber());
Console.WriteLine(test1.GenerateNumber());
Console.WriteLine(test1.GenerateNumber());
Console.WriteLine(test1.GenerateNumber());
Console.WriteLine(test1.FindMean());

//Абстрактный Класс Генератор - разделяет переменные и методы всех остальных классов
abstract class Generator
{
private string Name;

public Generator(string name)
{
Name = name;
}

public int FindMeanBase(int N, List<int> numbers)
{
int mean = 0;
for (int i = 0; i < N; i++)
{
mean += numbers[i];
}
return mean / N;
}

public virtual void PrintGenName()
{
Console.WriteLine($"{Name}");
}
}
//Абстрактный класс Одиночный Генератор - разделяет переменные и методы СлучайногоГенератора и ГенератораСпостояннымШагом
abstract class SoloGenerator : Generator
{

private List<int> numbers = new List<int>();
protected Random random = new Random();

public int number_of_iter;


public virtual int GenerateNumber()
{
return 1;
}

public List<int> Numbers
{
get
{
return numbers;
}
}


public SoloGenerator(string name, int number) : base(name)
{
number_of_iter = number;
}

protected void AddNewNum(int generated_value)
{
numbers.Add(generated_value);

}

public void UndoLastStep()
{
numbers.RemoveAt(numbers.Count() - 1);
}

public int FindMean()
{
return FindMeanBase(number_of_iter, numbers);
}

}
//Генератор случайных чисел
class RandomGenerator : SoloGenerator
{

private int LeftMargin, RightMargin;
public RandomGenerator(int number, int left_margin, int right_margin, string name) : base(name, number)
{
if (left_margin >= right_margin)
{
throw new ArgumentException("Invalid margins");
}
LeftMargin = left_margin;
RightMargin = right_margin;
}

public override void PrintGenName()
{
base.PrintGenName();
Console.WriteLine("Тип : Генератор случайных чисел");
}

public override int GenerateNumber()
{
int value = random.Next(LeftMargin, RightMargin);
base.AddNewNum(value);
return value;
}
}
//Пошаговый генератор
class StepGenerator : SoloGenerator
{
private int Step, Start;
public StepGenerator(int number, string name, int step, int start) : base(name, number)
{
Step = step;
Start = start;
}
public override void PrintGenName()
{
base.PrintGenName();
Console.WriteLine("Тип : Генератор случайных чисел");
}
public override int GenerateNumber()
{
if (Numbers.Count() == 0)
{
base.AddNewNum(Start + Step);
return Start + Step;
} else
{
int NewNum = Numbers.Last() + Step;
base.AddNewNum(NewNum);
return NewNum;
}
}
}
//Составной генератор
class CompositeGenerator : Generator
{
public CompositeGenerator(List<RandomGenerator> random_list, List<StepGenerator> step_list, int N, string name) : base(name)
{

}

public override void PrintGenName()
{
base.PrintGenName();
Console.WriteLine("Тип : Составной калькулятор");
}
}
