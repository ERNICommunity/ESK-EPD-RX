namespace RxDemo
{
    using System;
    using System.Collections.Immutable;
    using System.Reactive.Linq;

    public class MainDataContext : MainDataContextBase
    {
        public override IObservable<int> OddNumbersObservable
        {
            get
            {
                return NumbersObservable.Where(p => p % 2 != 0);
            }
        }

        public override IObservable<string> OrdinalNumbersObservable
        {
            get
            {
                return NumbersObservable.Select(
                    p =>
                    {
                        if (p == 0)
                        {
                            return "Nothing";
                        }

                        string prefix = "";
                        string suffix = "th";
                        if (p < 0)
                        {
                            prefix = "Negative ";
                        }

                        if (Math.Abs(p) % 10 == 1 && Math.Abs(p) / 10 != 1)
                        {
                            suffix = "st";
                        }

                        if (Math.Abs(p) % 10 == 2 && Math.Abs(p) / 10 != 1)
                        {
                            suffix = "nd";
                        }

                        return prefix + Math.Abs(p) + suffix;
                    });
            }
        }

        public override IObservable<ImmutableSortedDictionary<double, double>> GraphObservable
        {
            get
            {
                return NumbersObservable.Scan(ImmutableSortedDictionary<double, double>.Empty, (doubles, i) => doubles.SetItem(DateTime.Now.Ticks - new DateTime(2015, 1, 1).Ticks, i));
            }
        }
    }
}