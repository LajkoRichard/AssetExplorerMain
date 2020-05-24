using AssetExplorer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls.Primitives;
using Jib.WPF.Controls.DataGrid;

namespace AssetExplorer.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {
        #region Variables

        private AssetContext _context;

        public AssetContext Context
        {
            get { return _context; }
            set
            {
                _context = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Asset> _activeassets;

        public ObservableCollection<Asset> ActiveAssets
        {
            get { return _activeassets; }
            set
            {
                _activeassets = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Asset> _archiveassets;

        public ObservableCollection<Asset> ArchiveAssets
        {
            get { return _archiveassets; }
            set
            {
                _archiveassets = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Asset> _assetsToBeAdded;

        public ObservableCollection<Asset> AssetsToBeAdded
        {
            get { return _assetsToBeAdded; }
            set
            {
                _assetsToBeAdded = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Asset> _assetsToBeModified;

        public ObservableCollection<Asset> AssetsBeforeModification
        {
            get { return _assetsToBeModified; }
            set
            {
                _assetsToBeModified = value;
                OnPropertyChanged();
            }
        }

        private Asset _assetSelected;

        public Asset AssetSelected
        {
            get { return _assetSelected; }
            set
            {
                _assetSelected = value;
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
        }

        #endregion

        #region Commands

        private ICommand _savecommand;

        public ICommand SaveButtonCommand
        {
            get
            {
                return _savecommand ?? (_savecommand = new RelayCommand.RelayCommand(
                   x =>
                   {
                       SaveData();
                   }));
            }
        }

        private ICommand _deletecommand;

        public ICommand DeleteButtonCommand
        {
            get
            {
                return _deletecommand ?? (_deletecommand = new RelayCommand.RelayCommand(
                   x =>
                   {
                       DeleteData(x);
                   }));
            }
        }

        private ICommand _addcommand;

        public ICommand AddButtonCommand
        {
            get
            {
                return _addcommand ?? (_addcommand = new RelayCommand.RelayCommand(
                   x =>
                   {
                       AddData();
                   }));
            }
        }

        private ICommand _onassettobemodifiedcommand;

        public ICommand OnAssetToBeModifiedCommand
        {
            get
            {
                return _onassettobemodifiedcommand ?? (_onassettobemodifiedcommand = new RelayCommand.RelayCommand(
                   x =>
                   {
                       OnAssetToBeModified();
                   }));
            }
        }

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
                ActiveAssets = new ObservableCollection<Asset>(Context.Assets.Where(item => item.IsArchive == false).ToList());
                ArchiveAssets = new ObservableCollection<Asset>(Context.Assets.Where(item => item.IsArchive == true).ToList());
            }
            
        }

        private void OnAssetToBeModified()
        {
            Asset OriginalAsset = new Asset(AssetSelected.DeviceType, AssetSelected.Serial, AssetSelected.MAC, AssetSelected.User, AssetSelected.Knox, AssetSelected.Department, AssetSelected.Location, AssetSelected.IP, AssetSelected.Output, AssetSelected.Input, AssetSelected.Repair, AssetSelected.IsScrapped, true, AssetSelected.IsActive, AssetSelected.IsSelected, AssetSelected.IsModified);

            AssetsBeforeModification.Add(OriginalAsset);

            AssetSelected.IsModified = true;
        }

        private void DeleteData(object SelectedAssets)
        {
            if (MessageBox.Show("Are you sure that want to delete the selected data?", "Alert", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                //for (int i = 0; i < ActiveAssets.Count; i++)
                //{
                //    if (ActiveAssets[i].IsSelected)
                //    {
                //        ActiveAssets[i].IsArchive = true;
                //    }
                //}

                //ObservableCollection<Asset> SelectedAssetsCollection = SelectedAssets;

                //for (int i = 0; i < SelectedAssets.Count; i++)
                //{
                //    //SelectedAssets[i].IsArchive = true;
                //}

                System.Collections.IList items = (System.Collections.IList)SelectedAssets;
                var collection = items.Cast<Asset>();

                for (int i = 0; i < collection.Count(); i++)
                {
                    collection.ElementAt(i).IsArchive = true;
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
                    AssetsToBeAdded[i].IsArchive = false;
                    Context.Add(AssetsToBeAdded[i]);
                }

                Context.SaveChanges();
                AssetsToBeAdded.Clear();
                ActiveAssets = new ObservableCollection<Asset>(Context.Assets.Where(item => item.IsArchive == false).ToList());
            }
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
