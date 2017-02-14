using System;
using System.Collections.Generic;

namespace Composite
{
    class Program
    {
        static void Main(string[] args)
        {
            var root = new Composite("root");
            root.Add(new Leaf("A"));
            root.Add(new Leaf("B"));

            var composite = new Composite("Composite X");
            composite.Add(new Leaf("XA"));
            composite.Add(new Leaf("XB"));

            root.Add(composite);
            root.Add(new Leaf("C"));

            root.Display(2);
            Console.ReadLine();
        }
    }

    abstract class Component
    {
        protected string name;

        public Component(string name)
        {
            this.name = name;
        }

        public abstract void Add(Component c);
        public abstract void Remove(Component c);
        public abstract void Display(int depth);
    }

    class Composite : Component
    {
        private List<Component> _children = new List<Component>();

        public Composite(string name) : base(name) { }

        public override void Add(Component c)
        {
            _children.Add(c);
        }

        public override void Display(int depth)
        {
            Console.WriteLine(new String('-', depth) + name);
            foreach (var child in _children)
                child.Display(depth + 2);
        }

        public override void Remove(Component c)
        {
            _children.Remove(c);
        }
    }

    class Leaf : Component
    {
        public Leaf(string name) : base(name) { }

        public override void Add(Component c)
        {
            Console.WriteLine("Cannot add to a leaf");
        }

        public override void Display(int depth)
        {
            Console.WriteLine(new String('-', depth) + name);
        }

        public override void Remove(Component c)
        {
            Console.WriteLine("Cannot remove from a leaf");
        }
    }
}