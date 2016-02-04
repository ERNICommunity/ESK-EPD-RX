namespace RxDemo
{
    using System;
    using System.Collections.Immutable;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Reactive.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using System.Windows;

    using RxDemo.Annotations;

    public abstract class MainDataContextBase : INotifyPropertyChanged
    {
        private int _number;

        private int _oddNumber;

        private string _ordinalNumber;

        private ObservableCollection<SubItem> _dividedByModulo = new ObservableCollection<SubItem>();

        private ObservableCollection<SubItem> _dividedByModuloOnlyFirstFive = new ObservableCollection<SubItem>();

        private ObservableCollection<SubItem> _dividedByDivUntilNineObservable = new ObservableCollection<SubItem>();

        private ImmutableSortedDictionary<double, double> _graphPoints;

        private int _otherNumber;

        private int _otherOddNumber;

        public IObservable<int> NumbersObservable { get; private set; }

        public abstract IObservable<int> OddNumbersObservable { get; }

        public abstract IObservable<string> OrdinalNumbersObservable { get; }

        public abstract IObservable<ImmutableSortedDictionary<double, double>> GraphObservable { get; }

        public abstract IObservable<IObservable<int>> DividedByModuloObservable { get; }

        public abstract IObservable<IObservable<int>> DividedByModuloOnlyFirstFiveObservable { get; }

        public abstract IObservable<IObservable<int>> DividedByDivUntilNineObservable { get; }

        public abstract IObservable<int> OtherNumbersObservable { get; }

        public abstract IObservable<int> OtherOddNumbersObservable { get; }

        protected void Initialize()
        {
            var connectable = Observable.Create<int>(
                            async (obs, cancellationToken) =>
                            {
                                var rnd = new Random();
                                int number = 0;
                                while (true)
                                {
                                    await Task.Delay(rnd.Next(300, 1000));
                                    number += rnd.Next(-10, 11);
                                    obs.OnNext(number);
                                    if (cancellationToken.IsCancellationRequested)
                                    {
                                        break;
                                    }
                                }

                                obs.OnCompleted();
                            }).Publish();
            NumbersObservable = connectable;
            NumbersObservable.Subscribe(p => Number = p, ex => MessageBox.Show(ex.Message));
            SubscribeToObservables();
            connectable.Connect();
        }

        private void SubscribeToObservables()
        {
            OddNumbersObservable.Subscribe(p => OddNumber = p, ex => MessageBox.Show(ex.Message));
            OrdinalNumbersObservable.Subscribe(p => OrdinalNumber = p, ex => MessageBox.Show(ex.Message));
            GraphObservable.Subscribe(p => GraphPoints = p, ex => MessageBox.Show(ex.Message));
            DividedByModuloObservable.Select(p => new SubItem(p)).Subscribe(p =>
            {
                p.Observable.Subscribe(q => { }, () => _dividedByModulo.Remove(p));
                _dividedByModulo.Add(p);
            }, ex => MessageBox.Show(ex.Message));
            DividedByModuloOnlyFirstFiveObservable.Select(p => new SubItem(p)).Subscribe(p =>
            {
                p.Observable.Subscribe(q => { }, () => _dividedByModuloOnlyFirstFive.Remove(p));
                _dividedByModuloOnlyFirstFive.Add(p);
            }, ex => MessageBox.Show(ex.Message));
            DividedByDivUntilNineObservable.Select(p => new SubItem(p)).Subscribe(p =>
            {
                p.Observable.Subscribe(q => { }, () => _dividedByDivUntilNineObservable.Remove(p));
                _dividedByDivUntilNineObservable.Add(p);
            }, ex => MessageBox.Show(ex.Message));
            OtherNumbersObservable.Subscribe(p => OtherNumber = p, ex => MessageBox.Show(ex.Message));
            OtherOddNumbersObservable.Subscribe(p => OtherOddNumber = p, ex => MessageBox.Show(ex.Message));
        }

        public int Number
        {
            get { return _number; }
            set
            {
                _number = value;
                OnPropertyChanged();
            }
        }

        public int OddNumber
        {
            get
            {
                return _oddNumber;
            }
            set
            {
                _oddNumber = value;
                OnPropertyChanged();
            }
        }

        public string OrdinalNumber
        {
            get
            {
                return _ordinalNumber;
            }
            set
            {
                _ordinalNumber = value;
                OnPropertyChanged();
            }
        }

        public ImmutableSortedDictionary<double, double> GraphPoints
        {
            get
            {
                return _graphPoints;
            }
            set
            {
                _graphPoints = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<SubItem> DividedByModulo
        {
            get { return _dividedByModulo; }
        }

        public ObservableCollection<SubItem> DividedByModuloOnlyFirstFive
        {
            get { return _dividedByModuloOnlyFirstFive; }
        }

        public ObservableCollection<SubItem> DividedByDivUntilNine
        {
            get { return _dividedByDivUntilNineObservable; }
        }

        public int OtherNumber
        {
            get { return _number; }
            set
            {
                _number = value;
                OnPropertyChanged();
            }
        }

        public int OtherOddNumber
        {
            get
            {
                return _oddNumber;
            }
            set
            {
                _oddNumber = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}