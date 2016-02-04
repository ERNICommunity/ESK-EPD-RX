namespace RxDemo
{
    using System;
    using System.Collections.Immutable;
    using System.Reactive.Linq;
    using System.Reactive.Subjects;
    using System.Reactive.Threading.Tasks;
    using System.Threading.Tasks;

    public class MainDataContext : MainDataContextBase
    {
        private IConnectableObservable<int> _otherNumbersObs;

        public MainDataContext()
        {
            _otherNumbersObs = Observable.Create<int>(async obs =>
            {
                var rnd = new Random();
                while (true)
                {
                    await Task.Delay(1000);
                    obs.OnNext(rnd.Next(-100, 100));
                }
            }).Publish();
            _otherNumbersObs.Connect();

            Initialize();
        }

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

        public override IObservable<IObservable<int>> DividedByModuloObservable
        {
            get
            {
                return NumbersObservable.GroupBy(p => Math.Abs(p % 10));
            }
        }

        public override IObservable<IObservable<int>> DividedByModuloOnlyFirstFiveObservable
        {
            get
            {
                return NumbersObservable.GroupBy(p => Math.Abs(p % 10)).Select(p => p.Take(5));
            }
        }

        public override IObservable<IObservable<int>> DividedByDivUntilNineObservable
        {
            get
            {
                return NumbersObservable.GroupByUntil(p => p / 10, p => p.Where(q => Math.Abs(q % 10) == 9));
            }
        }

        public override IObservable<int> OtherNumbersObservable
        {
            get
            {
                return _otherNumbersObs;
            }
        }

        public override IObservable<int> OtherOddNumbersObservable
        {
            get
            {
                return _otherNumbersObs.Where(p => p % 2 != 0);
            }
        }

        public override IObservable<int> SumOfNumberAndOtherObservable
        {
            get
            {
                return NumbersObservable.CombineLatest(_otherNumbersObs, (p, q) => p + q);
            }
        }

        public override IObservable<int> ComputationsForOtherNumbersMerged
        {
            get
            {
                return OtherNumbersObservable.SelectMany(p => Compute(p));
            }
        }

        public override IObservable<int> ComputationsForOtherNumbersSwitched
        {
            get
            {
                return OtherNumbersObservable.Select(p => Compute(p).ToObservable()).Switch();
            }
        }

        public override IObservable<int> ComputationsForOtherNumbersConcatted
        {
            get
            {
                return OtherNumbersObservable.Select(p => Compute(p).ToObservable()).Concat();
            }
        }
    }
}