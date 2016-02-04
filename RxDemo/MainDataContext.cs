namespace RxDemo
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Reactive.Linq;
    using System.Reactive.Subjects;
    using System.Reactive.Threading.Tasks;
    using System.Threading.Tasks;

    public class MainDataContext : MainDataContextBase
    {
        public MainDataContext()
        {
            Initialize();
        }

        public override IObservable<int> OddNumbersObservable
        {
            get
            {
                return Observable.Return(0);
            }
        }

        public override IObservable<string> OrdinalNumbersObservable
        {
            get
            {
                return Observable.Return("");
            }
        }

        public override IObservable<ImmutableSortedDictionary<double, double>> GraphObservable
        {
            get
            {
                return Observable.Return(new Dictionary<double, double> { { 0, 0 }, { 1, 1 } }.ToImmutableSortedDictionary());
            }
        }

        public override IObservable<IObservable<int>> DividedByModuloObservable
        {
            get
            {
                return Observable.Return(Observable.Return(0));
            }
        }

        public override IObservable<IObservable<int>> DividedByModuloOnlyFirstFiveObservable
        {
            get
            {
                return Observable.Return(Observable.Return(0));
            }
        }

        public override IObservable<IObservable<int>> DividedByDivUntilNineObservable
        {
            get
            {
                return Observable.Return(Observable.Return(0));
            }
        }

        public override IObservable<int> OtherNumbersObservable
        {
            get
            {
                return Observable.Return(0);
            }
        }

        public override IObservable<int> OtherOddNumbersObservable
        {
            get
            {
                return Observable.Return(0);
            }
        }

        public override IObservable<int> SumOfNumberAndOtherObservable
        {
            get
            {
                return Observable.Return(0);
            }
        }

        public override IObservable<int> ComputationsForOtherNumbersMerged
        {
            get
            {
                return Observable.Return(0);
            }
        }

        public override IObservable<int> ComputationsForOtherNumbersSwitched
        {
            get
            {
                return Observable.Return(0);
            }
        }

        public override IObservable<int> ComputationsForOtherNumbersConcatted
        {
            get
            {
                return Observable.Return(0);
            }
        }
    }
}