using System;
using System.Collections.Generic;

namespace Observer
{
    class Program
    {
        static void Main(string[] args)
        {
            var teacher = new Teacher();
            var student = new Student("Nick");
            var student2 = new Student("Barry");
            teacher.AddObserver(student);
            teacher.AddObserver(student2);

            teacher.Speak();

            Console.ReadLine();
        }
    }

    class Student : IObserver
    {
        private string name;
        public Student(string name)
        {
            this.name = name;
        }

        public void Update()
        {
            Console.WriteLine($"{name}: The teacher has spoken!");
        }
    }

    class Teacher : IObservable
    {
        public List<IObserver> Observers { get; } = new List<IObserver>();

        public void AddObserver(IObserver observer)
        {
            Observers.Add(observer);
        }

        public void RemoveObserver(IObserver observer)
        {
            Observers.Remove(observer);
        }

        public void Speak()
        {
            foreach (var observer in Observers)
                observer.Update();
        }
    }

    interface IObserver
    {
        void Update();
    }

    interface IObservable
    {
        List<IObserver> Observers { get; }
        void AddObserver(IObserver observer);
        void RemoveObserver(IObserver observer);
    }
}