﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using Adaptive.ReactiveTrader.Client.Models;
using Adaptive.ReactiveTrader.Client.Repositories;
using Adaptive.ReactiveTrader.Shared.UI;
using log4net;
using PropertyChanged;

namespace Adaptive.ReactiveTrader.Client.UI.SpotTiles
{
    [ImplementPropertyChanged]
    public class SpotTilesViewModel : ViewModelBase, ISpotTilesViewModel
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof (SpotTilesViewModel));

        public ObservableCollection<ISpotTileViewModel> SpotTiles { get; private set; }
        private readonly IReferenceDataRepository _referenceDataRepository;
        private readonly Func<ICurrencyPair, ISpotTileViewModel> _spotTileFactory;

        public SpotTilesViewModel(IReferenceDataRepository referenceDataRepository,
            Func<ICurrencyPair, ISpotTileViewModel> spotTileFactory)
        {
            _referenceDataRepository = referenceDataRepository;
            _spotTileFactory = spotTileFactory;

            SpotTiles = new ObservableCollection<ISpotTileViewModel>();
            LoadSpotTiles();
        }

        private void LoadSpotTiles()
        {
            _referenceDataRepository.GetCurrencyPairs()
                .Take(1) // remove this to handle updates
                .ObserveOnDispatcher()
                .Subscribe(
                    currencyPairs => currencyPairs.ForEach(CreateSpotTile),
                    error => Log.Error("Failed to get currencies", error));
        }

        private void CreateSpotTile(ICurrencyPair currencyPair)
        {
            var spotTile = _spotTileFactory(currencyPair);
            SpotTiles.Add(spotTile);
        }
    }
}