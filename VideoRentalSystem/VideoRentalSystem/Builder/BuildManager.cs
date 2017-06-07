using Ninject.Modules;
using VideoRentalSystem.Commands.AddCommands;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Commands.CreateCommands;
using VideoRentalSystem.Commands.Factory;
using VideoRentalSystem.Commands.ListingCommands;
using VideoRentalSystem.Commands.RemoveCommands;
using VideoRentalSystem.Commands.UpdateCommands;
using VideoRentalSystem.Common;
using VideoRentalSystem.Common.Contracts;
using VideoRentalSystem.Core;
using VideoRentalSystem.Core.Contracts;
using VideoRentalSystem.Data;
using VideoRentalSystem.Data.Contracts;
using VideoRentalSystem.Data.Postgre;
using VideoRentalSystem.Data.Postgre.Contracts;
using VideoRentalSystem.Models.Factories;

namespace VideoRentalSystem.Builder
{
    public class BuildManager : NinjectModule
    {
        public override void Load()
        {
            var consoleReader = this.Bind<IReader>().To<ConsoleReader>().InSingletonScope();
            var consoleWriter = this.Bind<IWriter>().To<ConsoleWriter>().InSingletonScope();
            var videoRentalContext = this.Bind<VideoRentalContext>().ToSelf().InSingletonScope();

            var database = this.Bind<IDatabase>().To<Database>().InSingletonScope();
            this.Bind<IDatabasePostgre>().To<DatabasePostgre>().InSingletonScope();
            var modelFactory = this.Bind<IModelsFactory>().To<ModelsFactory>().InSingletonScope();

            this.Bind<IServiceLocator>().To<ServiceLocator>().InSingletonScope();
            var commandFactory = this.Bind<ICommandsFactory>().To<CommandsFactory>().InSingletonScope();
            var commandProcesor = this.Bind<IProcessor>().To<CommandProcessor>().InSingletonScope();

            /////Bind commands
            this.Bind<ICommand>().To<CreateCountryCommand>().Named("CreateCountry");
            this.Bind<ICommand>().To<CountryDetailsCommand>().Named("CountryDetails");
            this.Bind<ICommand>().To<ListAllCountriesCommand>().Named("ListAllCountries");
            this.Bind<ICommand>().To<UpdateCountryCommand>().Named("UpdateCountry");

            this.Bind<ICommand>().To<CreateTownCommand>().Named("CreateTown");
            this.Bind<ICommand>().To<TownDetailsCommand>().Named("TownDetails");
            this.Bind<ICommand>().To<ListAllTownsCommand>().Named("ListAllTowns");
            this.Bind<ICommand>().To<UpdateTownCommand>().Named("UpdateTown");

            this.Bind<ICommand>().To<CreateAddressCommand>().Named("CreateAddress");
            this.Bind<ICommand>().To<AddressDetailsCommand>().Named("AddressDetails");
            this.Bind<ICommand>().To<ListAllAddressesCommand>().Named("ListAllAddresses");
            this.Bind<ICommand>().To<UpdateAddressCommand>().Named("UpdateAddress");

            this.Bind<ICommand>().To<CreateEmployeeCommand>().Named("CreateEmployee");
            this.Bind<ICommand>().To<ListAllEmployeesCommand>().Named("ListAllEmployees");
            this.Bind<ICommand>().To<LoadEmployeeFromJSONCommand>().Named("LoadEmployeeFromJSON");
            this.Bind<ICommand>().To<UpdateEmployeeCommand>().Named("UpdateEmployee");
            this.Bind<ICommand>().To<RemoveEmployeeCommand>().Named("RemoveEmployee");

            this.Bind<ICommand>().To<CreateManagerCommand>().Named("CreateManager");
            this.Bind<ICommand>().To<RemoveManagerCommand>().Named("RemoveManager");
            this.Bind<ICommand>().To<UpdateManagerCommand>().Named("UpdateManager");

            this.Bind<ICommand>().To<CreateCustomerCommand>().Named("CreateCustomer");
            this.Bind<ICommand>().To<ListAllCustomersCommand>().Named("ListAllCustomers");
            this.Bind<ICommand>().To<LoadCustomerFromJSONCommand>().Named("LoadCustomerFromJSON");

            this.Bind<ICommand>().To<CreateReviewCommand>().Named("CreateReview");
            this.Bind<ICommand>().To<ListAllReviewsCommand>().Named("ListAllReviews");
            this.Bind<ICommand>().To<LoadReviewFromJSONCommand>().Named("LoadReveiwFromJSON");

            this.Bind<ICommand>().To<CreateFilmCommand>().Named("CreateFilm");

            // this.Bind<ICommand>().To<AddFilmCategory>().Named("AddFilmCategory");
            this.Bind<ICommand>().To<CreateFilmRating>().Named("CreateFilmRating");
            this.Bind<ICommand>().To<CreateFilmGenre>().Named("CreateFilmGenre");
            this.Bind<ICommand>().To<LoadFilmGenresFromJSONCommand>().Named("LoadFilmGenreFromJSON");
            this.Bind<ICommand>().To<ListAllGenresCommand>().Named("ListAllFilmGenres");

            this.Bind<ICommand>().To<CreateAwardCommand>().Named("CreateAward");
            this.Bind<ICommand>().To<AddAwardFilmCommand>().Named("AddAwardToFilm");
            this.Bind<ICommand>().To<AddGenreFilmCommand>().Named("AddGenreToFilm");
            this.Bind<ICommand>().To<AddRatingFilmCommand>().Named("AddRatingToFilm");

            this.Bind<ICommand>().To<CreateStoreCommand>().Named("CreateStore");
            this.Bind<ICommand>().To<ListAllStoresCommand>().Named("ListAllStores");
            this.Bind<ICommand>().To<StoreDetailsCommand>().Named("StoreDetails");
            this.Bind<ICommand>().To<AddStoreEmployeeCommand>().Named("AddStoreEmployee");
            this.Bind<ICommand>().To<RemoveStoreEmployeeCommand>().Named("RemoveStoreEmployee");

            this.Bind<ICommand>().To<CreateStorageCommand>().Named("CreateStorage");
            this.Bind<ICommand>().To<ListAllStoragesCommand>().Named("ListAllStorages");
            this.Bind<ICommand>().To<StorageDetailsCommand>().Named("StorageDetails");
            this.Bind<ICommand>().To<AddFilmQuantityCommand>().Named("AddFilmQuantity");
            this.Bind<ICommand>().To<RemoveFilmQuantityCommand>().Named("RemoveFilmQuantity");

            this.Bind<ICommand>().To<CreateFilmStaffCommand>().Named("CreateFilmStaff");
            this.Bind<ICommand>().To<ListAllFilmStaffsCommand>().Named("ListAllFilmStaffs");

            this.Bind<ICommand>().To<CreateTarifCommand>().Named("CreateTarif");
            this.Bind<ICommand>().To<TarifDetailsCommand>().Named("TarifDetails");
            this.Bind<ICommand>().To<ListAllTarifsCommand>().Named("ListAllTarifs");
            this.Bind<ICommand>().To<ListAllTarifsByTypeCommand>().Named("ListAllTarifsByType");
            this.Bind<ICommand>().To<RemoveTarifCommand>().Named("RemoveTarif");
            this.Bind<ICommand>().To<UpdateTarifCommand>().Named("UpdateTarif");

            this.Bind<ICommand>().To<CreateLoanCommand>().Named("CreateLoan");

            var engine = this.Bind<IEngine>().To<Engine>().InSingletonScope();
        }
    }
}
