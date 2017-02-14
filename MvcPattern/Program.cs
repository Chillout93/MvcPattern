using System;
using System.Collections.Generic;

namespace MvcPattern
{
    public class Program
    {
        static void Main(string[] args)
        {
            var model = new Model();
            var view = new View();
            var controller = new Controller(model, view);

            view.SetController(controller);
            model.AddObserver(controller);
            model.AddObserver(view);

            while(true)
            {
                view.ClickButton();
            }
        }
    }

    public class Model : IObservable
    {
        public List<IObserver> Observers { get; set; } = new List<IObserver>();
        public int Count { get; private set; }

        public Model()
        {
            Count = 0;
        }

        public void AddObserver(IObserver observer)
        {
            Observers.Add(observer);
        }

        public void RemoveObserver(IObserver observer)
        {
            Observers.Add(observer);
        }
        
        public void OnButtonClick()
        {
            Count++;
            foreach (var observer in Observers)
                observer.Update(Count);
        }
    }
    
    public class Controller : IObserver, IController
    {
        private Model model;
        private View view;

        public Controller(Model model, View view)
        {
            this.model = model;
            this.view = view;
        }

        public bool OnButtonClick()
        {
            if (model.Count >= 10)
                return false;

            model.OnButtonClick();
            return true;
        }

        public void Update(int count)
        {
            
        }
    }

    public class View : IObserver
    {
        private Controller controller;
        public View(){}

        public void SetController(Controller controller)
        {
            this.controller = controller;
        }

        public void ClickButton()
        {
            Console.WriteLine("Do you want to click the button? (Y/N): ");
            var answer = Console.ReadLine();

            bool? result = null;
            if (answer.ToLower() == "y")
                result = controller.OnButtonClick();

            Console.WriteLine((result.HasValue) ? (result.Value) ? "The count updated!" : "You can't update the count anymore, it's maximum is reached." : "The count did not update.");
        }
        public void Update(int count)
        {
            Console.WriteLine($"View: {count}");
        }
    }

    public interface IController
    {
        bool OnButtonClick();
    }

    public interface IObserver
    {
        void Update(int count);
    }

    public interface IObservable
    {
        List<IObserver> Observers { get; }
        void AddObserver(IObserver observer);
        void RemoveObserver(IObserver observer);
    }
}