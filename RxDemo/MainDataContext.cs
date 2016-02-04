namespace RxDemo
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Reactive.Linq;

    public class MainDataContext : MainDataContextBase
    {
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
    }
}