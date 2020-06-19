using AssetExplorer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Diagnostics;
using System.Net.NetworkInformation;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections;

namespace AssetExplorer.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {
        #region Variables

        private AssetContext _context;

        public AssetContext Context
        {
            get => _context;
            set
            {
                _context = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Asset> _activeassets;

        public ObservableCollection<Asset> ActiveAssets
        {
            get => _activeassets;
            set
            {
                _activeassets = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Asset> _archiveassets;

        public ObservableCollection<Asset> ArchiveAssets
        {
            get => _archiveassets;
            set
            {
                _archiveassets = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Asset> _assetsToBeAdded;

        public ObservableCollection<Asset> AssetsToBeAdded
        {
            get => _assetsToBeAdded;
            set
            {
                _assetsToBeAdded = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Asset> _assetsToBeModified;

        public ObservableCollection<Asset> AssetsBeforeModification
        {
            get => _assetsToBeModified;
            set
            {
                _assetsToBeModified = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _availableLocations;

        public ObservableCollection<string> AvailableLocations
        {
            get => _availableLocations;
            set
            {
                _availableLocations = value;
                OnPropertyChanged();
            }
        }

        private Asset _assetSelected;

        public Asset AssetSelected
        {
            get => _assetSelected;
            set
            {
                _assetSelected = value;
                OnPropertyChanged();
            }
        }

        private object _lockObject;

        public object LockObject
        {
            get => _lockObject;
            set => _lockObject = value;
        }

        private Stopwatch _stopWatch;

        public Stopwatch StopWatch
        {
            get => _stopWatch;
            set => _stopWatch = value;
        }

        private TimeSpan _timeSpan;

        public TimeSpan TimeSpan
        {
            get => _timeSpan;
            set => _timeSpan = value;
        }

        private int _nFound;

        public int NFound
        {
            get => _nFound;
            set => _nFound = value;
        }

        private GridLength _rowHeight;

        public GridLength RowHeight
        {
            get => _rowHeight;
            set
            {
                _rowHeight = value;
                OnPropertyChanged();
            }
        }

        private bool _isScrappedSelected;

        public bool IsScrappedSelected
        {
            get => _isScrappedSelected;
            set
            {
                _isScrappedSelected = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Constructor

        public MainViewModel()
        {
            Context = new AssetContext();
            ActiveAssets = new ObservableCollection<Asset>(Context.Assets.Where(item => item.IsArchive == false).ToList());
            ArchiveAssets = new ObservableCollection<Asset>(Context.Assets.Where(item => item.IsArchive == true).ToList());
            AssetsToBeAdded = new ObservableCollection<Asset>();
            AssetsBeforeModification = new ObservableCollection<Asset>();
            AssetSelected = new Asset();
            LockObject = new object();
            StopWatch = new Stopwatch();
            NFound = 0;
            RowHeight = new GridLength(1.0, GridUnitType.Auto);
            AvailableLocations = new ObservableCollection<string>(ActiveAssets.Select(x => x.Location).ToList());
        }

        #endregion

        #region Commands

        private ICommand _savecommand;

        public ICommand SaveButtonCommand => _savecommand ?? (_savecommand = new RelayCommand.RelayCommand(
                   x =>
                   {
                       SaveData();
                   }));

        private ICommand _deletecommand;

        public ICommand DeleteButtonCommand => _deletecommand ?? (_deletecommand = new RelayCommand.RelayCommand(
                   x =>
                   {
                       DeleteData();
                   }));

        private ICommand _addcommand;

        public ICommand AddButtonCommand => _addcommand ?? (_addcommand = new RelayCommand.RelayCommand(
                   x =>
                   {
                       AddData();
                   }));

        private ICommand _onassettobemodifiedcommand;

        public ICommand OnAssetToBeModifiedCommand => _onassettobemodifiedcommand ?? (_onassettobemodifiedcommand = new RelayCommand.RelayCommand(
                   x =>
                   {
                       OnAssetToBeModified();
                   }));

        private ICommand _selectallcommand;

        public ICommand SelectAllCommand => _selectallcommand ?? (_selectallcommand = new RelayCommand.RelayCommand(
                   x =>
                   {
                       SelectAll(x as bool?);
                   }));

        private ICommand _scrapselectedcommand;

        public ICommand ScrapSelectedCommand => _scrapselectedcommand ?? (_scrapselectedcommand = new RelayCommand.RelayCommand(
                   x =>
                   {
                       ScrapSelected(x as object);
                   }));

        private ICommand _checkselectioncommand;

        public ICommand CheckSelectionCommand => _checkselectioncommand ?? (_checkselectioncommand = new RelayCommand.RelayCommand(
                   x =>
                   {
                       CheckSelection(x as object);
                   }));

        private ICommand _uncheckselectioncommand;

        public ICommand UnCheckSelectionCommand => _uncheckselectioncommand ?? (_uncheckselectioncommand = new RelayCommand.RelayCommand(
                   x =>
                   {
                       UnCheckSelection(x as object);
                   }));

        private ICommand _reloaddatacommand;

        public ICommand ReloadDataCommand => _reloaddatacommand ?? (_reloaddatacommand = new RelayCommand.RelayCommand(
                   x =>
                   {
                       ReloadData();
                   }));

        private ICommand _pingbuttoncommand;

        public ICommand PingButtonCommand => _pingbuttoncommand ?? (_pingbuttoncommand = new RelayCommand.RelayCommand(
                   x =>
                   {
                       Ping();
                   }));

        private ICommand _onexpanderexpandedcommand;

        public ICommand OnExpanderExpandedCommand => _onexpanderexpandedcommand ?? (_onexpanderexpandedcommand = new RelayCommand.RelayCommand(
                   x =>
                   {
                       OnExpanderExpanded();
                   }));

        private ICommand _onexpandercollapsedcommand;

        public ICommand OnExpanderCollapsedCommand => _onexpandercollapsedcommand ?? (_onexpandercollapsedcommand = new RelayCommand.RelayCommand(
                   x =>
                   {
                       OnExpanderCollapsed();
                   }));

        #endregion

        #region Functions

        private void SaveData()
        {
            if (MessageBox.Show("Are you sure that you want to save the data", "Alert", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                for (int i = 0; i < ActiveAssets.Count; i++)
                {
                    if (ActiveAssets[i].IsModified)
                    {
                        Context.Update(ActiveAssets[i]);
                    }
                }

                for (int i = 0; i < AssetsBeforeModification.Count; i++)
                {
                    Context.Add(AssetsBeforeModification[i]);
                }

                Context.SaveChanges();
                AssetsBeforeModification.Clear();
                IsScrappedSelected = false;
                ActiveAssets = new ObservableCollection<Asset>(Context.Assets.Where(item => item.IsArchive == false).ToList());
                ArchiveAssets = new ObservableCollection<Asset>(Context.Assets.Where(item => item.IsArchive == true).ToList());
            }
            
        }

        private void OnAssetToBeModified()
        {
            if (!AssetsBeforeModification.Any(i => i.MAC == AssetSelected.MAC))
            {
                Asset OriginalAsset = new Asset(AssetSelected.DeviceType, AssetSelected.Serial, AssetSelected.MAC, AssetSelected.User, AssetSelected.Knox, AssetSelected.Department, AssetSelected.Location, AssetSelected.IP, AssetSelected.Output, AssetSelected.Input, AssetSelected.Repair, AssetSelected.IsScrapped, true, AssetSelected.IsActive, AssetSelected.LastActiveTime, AssetSelected.IsSelected, AssetSelected.IsModified);

                AssetsBeforeModification.Add(OriginalAsset);

                AssetSelected.IsModified = true;
            }
        }

        private void DeleteData()
        {
            if (MessageBox.Show("Are you sure that want to delete the selected data?", "Alert", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                for (int i = 0; i < ActiveAssets.Count; i++)
                {
                    if (ActiveAssets[i].IsSelected)
                    {
                        ActiveAssets[i].IsArchive = true;
                    }
                }

                Context.SaveChanges();
                ActiveAssets = new ObservableCollection<Asset>(Context.Assets.Where(item => item.IsArchive == false).ToList());
                ArchiveAssets = new ObservableCollection<Asset>(Context.Assets.Where(item => item.IsArchive == true).ToList());
            }
            
        }

        private void AddData()
        {
            if (MessageBox.Show("Are you sure that want to add the data?", "Alert", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                for (int i = 0; i < AssetsToBeAdded.Count; i++)
                {
                    Asset SavedAsset = ActiveAssets.Where(x => x.Serial == AssetsToBeAdded[i].Serial).FirstOrDefault();
                    if (SavedAsset != null)
                    {
                        SavedAsset.IsArchive = true;
                        Context.Add(AssetsToBeAdded[i]);
                        continue;
                    }

                    AssetsToBeAdded[i].IsArchive = false;
                    Context.Add(AssetsToBeAdded[i]);
                }

                Context.SaveChanges();
                AssetsToBeAdded.Clear();
                ActiveAssets = new ObservableCollection<Asset>(Context.Assets.Where(item => item.IsArchive == false).ToList());
            }
        }

        private void SelectAll(bool? checkstatus)
        {
            for (int i = 0; i < ActiveAssets.Count; i++)
            {
                ActiveAssets[i].IsSelected = (bool)checkstatus;
            }
        }

        private void ScrapSelected(object selectedrows)
        {
            var collection = (IList)selectedrows;
            for (int i = 0; i < collection.Count; i++)
            {
                (collection[i] as Asset).IsScrapped = IsScrappedSelected;
            }
        }

        private void CheckSelection(object selectedrows)
        {
            var collection = (IList)selectedrows;
            for (int i = 0; i < collection.Count; i++)
            {
                (collection[i] as Asset).IsSelected = true;
            }
        }

        private void UnCheckSelection(object selectedrows)
        {
            var collection = (IList)selectedrows;
            for (int i = 0; i < collection.Count; i++)
            {
                (collection[i] as Asset).IsSelected = false;
            }
        }

        private void ReloadData()
        {
            ActiveAssets = new ObservableCollection<Asset>(Context.Assets.Where(item => item.IsArchive == false).ToList());
            ArchiveAssets = new ObservableCollection<Asset>(Context.Assets.Where(item => item.IsArchive == true).ToList());
        }

        private async void Ping()
        {
            NFound = 0;

            var tasks = new List<Task>();

            StopWatch.Start();

            for (int i = 0; i < ActiveAssets.Count; i++)
            {
                Ping p = new Ping();
                var task = PingAndUpdateAsync(p, ActiveAssets[i]);
                tasks.Add(task);
            }

            await Task.WhenAll(tasks).ContinueWith(t =>
            {
                StopWatch.Stop();
                TimeSpan = StopWatch.Elapsed;
                MessageBox.Show(NFound.ToString() + " devices found! Elapsed time: " + TimeSpan.ToString(), "Asynchronous");
            });
        }

        private async Task PingAndUpdateAsync(Ping ping, Asset asset)
        {
            var reply = await ping.SendPingAsync(asset.IP, 100);

            if (reply.Status == IPStatus.Success)
            {
                lock (LockObject)
                {
                    asset.IsActive = true;
                    asset.LastActiveTime = DateTime.Now;
                    Context.SaveChanges();
                    NFound++;
                }
            }
            else
            {
                lock (LockObject)
                {
                    asset.IsActive = false;
                    Context.SaveChanges();
                }
            }
        }

        private void OnExpanderExpanded()
        {
            RowHeight = new GridLength(1.0, GridUnitType.Star);
        }

        private void OnExpanderCollapsed()
        {
            RowHeight = new GridLength(1.0, GridUnitType.Auto);
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion

    }
}
