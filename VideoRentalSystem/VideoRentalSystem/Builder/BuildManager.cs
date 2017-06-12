﻿using Ninject.Modules;
using VideoRentalSystem.Commands.AddCommands;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Commands.CreateCommands;
using VideoRentalSystem.Commands.Factory;
using VideoRentalSystem.Commands.ListingCommands;
using VideoRentalSystem.Commands.PdfPrintCommands;
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
using VideoRentalSystem.Data.SqLite;
using VideoRentalSystem.Data.SqLite.Contracts;
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
            this.Bind<IDatabaseLite>().To<DatabaseLite>().InSingletonScope();
            var modelFactory = this.Bind<IModelsFactory>().To<ModelsFactory>().InSingletonScope();

            this.Bind<IServiceLocator>().To<ServiceLocator>().InSingletonScope();
            var commandFactory = this.Bind<ICommandsFactory>().To<CommandsFactory>().InSingletonScope();
            var commandProcesor = this.Bind<IProcessor>().To<CommandProcessor>().InSingletonScope();

            /////Bind commands
            this.Bind<ICommand>().To<CreateCountryCommand>().Named("CreateCountry");
            this.Bind<ICommand>().To<CountryDetailsCommand>().Named("CountryDetails");
            this.Bind<ICommand>().To<ListAllCountriesCommand>().Named("ListAllCountries");
            this.Bind<ICommand>().To<ListAllCountriesToPdfCommand>().Named("ListAllCountriesToPdf");
            this.Bind<ICommand>().To<UpdateCountryCommand>().Named("UpdateCountry");
            this.Bind<ICommand>().To<LoadCountriesFromXMLCommand>().Named("LoadCountriesFromXML");

            this.Bind<ICommand>().To<CreateTownCommand>().Named("CreateTown");
            this.Bind<ICommand>().To<TownDetailsCommand>().Named("TownDetails");
            this.Bind<ICommand>().To<ListAllTownsCommand>().Named("ListAllTowns");
            this.Bind<ICommand>().To<ListAllTownsToPdfCommand>().Named("ListAllTownsToPdf");
            this.Bind<ICommand>().To<UpdateTownCommand>().Named("UpdateTown");

            this.Bind<ICommand>().To<CreateAddressCommand>().Named("CreateAddress");
            this.Bind<ICommand>().To<AddressDetailsCommand>().Named("AddressDetails");
            this.Bind<ICommand>().To<ListAllAddressesCommand>().Named("ListAllAddresses");
            this.Bind<ICommand>().To<ListAllAddressesToPdfCommand>().Named("ListAllAddressesToPdf");
            this.Bind<ICommand>().To<UpdateAddressCommand>().Named("UpdateAddress");

            this.Bind<ICommand>().To<CreateEmployeeCommand>().Named("CreateEmployee");
            this.Bind<ICommand>().To<ListAllEmployeesToPdfCommand>().Named("ListAllEmployeesToPdf");
            this.Bind<ICommand>().To<ListAllEmployeesCommand>().Named("ListAllEmployees");
            this.Bind<ICommand>().To<LoadEmployeeFromJSONCommand>().Named("LoadEmployeesFromJSON");
            this.Bind<ICommand>().To<UpdateEmployeeCommand>().Named("UpdateEmployee");
            this.Bind<ICommand>().To<RemoveEmployeeCommand>().Named("RemoveEmployee");

            this.Bind<ICommand>().To<CreateManagerCommand>().Named("CreateManager");
            this.Bind<ICommand>().To<RemoveManagerCommand>().Named("RemoveManager");
            this.Bind<ICommand>().To<UpdateManagerCommand>().Named("UpdateManager");

            this.Bind<ICommand>().To<CreateCustomerCommand>().Named("CreateCustomer");
            this.Bind<ICommand>().To<AddFilmToCustomerCommand>().Named("AddFilmToCustomer");
            this.Bind<ICommand>().To<AddGenreToCustomerCommand>().Named("AddGenreToCustomer");
            this.Bind<ICommand>().To<ListAllCustomersCommand>().Named("ListAllCustomers");
            this.Bind<ICommand>().To<ListAllCustomersToPdfCommand>().Named("ListAllCustomersToPdf");
            this.Bind<ICommand>().To<LoadCustomerFromJSONCommand>().Named("LoadCustomersFromJSON");
            this.Bind<ICommand>().To<RemoveCustomerCommand>().Named("RemoveCustomer");

            this.Bind<ICommand>().To<CreateReviewCommand>().Named("CreateReview");
            this.Bind<ICommand>().To<ListAllReviewsCommand>().Named("ListAllReviews");
            this.Bind<ICommand>().To<ListAllReviewsToPdfCommand>().Named("ListAllReviewsToPdf");
            this.Bind<ICommand>().To<LoadReviewFromJSONCommand>().Named("LoadReviewsFromJSON");
            this.Bind<ICommand>().To<RemoveReviewCommand>().Named("RemoveReview");

            this.Bind<ICommand>().To<CreateFilmCommand>().Named("CreateFilm");
            this.Bind<ICommand>().To<ListAllFilmsCommand>().Named("ListAllFilms");
            this.Bind<ICommand>().To<ListAllFilmdsToPdfCommand>().Named("ListAllFilmdsToPdf");

            // FILM BINDINGS
            this.Bind<ICommand>().To<CreateAwardCommand>().Named("CreateAward");
            this.Bind<ICommand>().To<CreateOrganisationCommand>().Named("CreateFilmAwardOrganisation");
            this.Bind<ICommand>().To<CreateFilmRatingCommand>().Named("CreateFilmRating");
            this.Bind<ICommand>().To<CreateFilmGenreCommand>().Named("CreateFilmGenre");
            this.Bind<ICommand>().To<LoadFilmGenresFromJSONCommand>().Named("LoadFilmGenresFromJSON");
            this.Bind<ICommand>().To<ListAllGenresCommand>().Named("ListAllFilmGenres");

            ////this.Bind<ICommand>().To<AddAwardFilmCommand>().Named("AddAwardToFilm");
            this.Bind<ICommand>().To<AddGenreFilmCommand>().Named("AddGenreToFilm");
            this.Bind<ICommand>().To<AddRatingFilmCommand>().Named("AddRatingToFilm");
            this.Bind<ICommand>().To<UpdateAwardCommand>().Named("UpdateAward");
            this.Bind<ICommand>().To<UpdateFilmCommand>().Named("UpdateFilm");
            this.Bind<ICommand>().To<UpdateFilmGenreCommand>().Named("UpdateGenre");
            this.Bind<ICommand>().To<UpdateFilmRatingCommand>().Named("UpdateRating");
            this.Bind<ICommand>().To<RemoveAwardCommand>().Named("RemoveAward");
            this.Bind<ICommand>().To<RemoveOrganisationCommand>().Named("RemoveFilmAwardOrganisation");
            this.Bind<ICommand>().To<RemoveFilmRatingCommand>().Named("RemoveRating");
            this.Bind<ICommand>().To<RemoveFilmGenreCommand>().Named("RemoveGenre");
            this.Bind<ICommand>().To<RemoveFilmCommand>().Named("RemoveFilm");

            this.Bind<ICommand>().To<CreateStoreCommand>().Named("CreateStore");
            this.Bind<ICommand>().To<ListAllStoresCommand>().Named("ListAllStores");
            this.Bind<ICommand>().To<ListAllStoresToPdfCommand>().Named("ListAllStoresToPdf");
            this.Bind<ICommand>().To<StoreDetailsCommand>().Named("StoreDetails");
            this.Bind<ICommand>().To<AddStoreEmployeeCommand>().Named("AddStoreEmployee");
            this.Bind<ICommand>().To<RemoveStoreEmployeeCommand>().Named("RemoveStoreEmployee");

            this.Bind<ICommand>().To<CreateStorageCommand>().Named("CreateStorage");
            this.Bind<ICommand>().To<ListAllStoragesCommand>().Named("ListAllStorages");
            this.Bind<ICommand>().To<ListAllStoragesToPdfCommand>().Named("ListAllStoragesToPdf");
            this.Bind<ICommand>().To<StorageDetailsCommand>().Named("StorageDetails");
            this.Bind<ICommand>().To<AddFilmQuantityCommand>().Named("AddFilmQuantity");
            this.Bind<ICommand>().To<RemoveFilmQuantityCommand>().Named("RemoveFilmQuantity");

            this.Bind<ICommand>().To<CreateFilmStaffCommand>().Named("CreateFilmStaff");
            this.Bind<ICommand>().To<ListAllFilmStaffsCommand>().Named("ListAllFilmStaffs");
            this.Bind<ICommand>().To<AddFilmStaffCommand>().Named("AddFilmStaff");

            this.Bind<ICommand>().To<CreateTarifCommand>().Named("CreateTarif");
            this.Bind<ICommand>().To<TarifDetailsCommand>().Named("TarifDetails");
            this.Bind<ICommand>().To<ListAllTarifsCommand>().Named("ListAllTarifs");
            this.Bind<ICommand>().To<ListAllTarifsToPdfCommand>().Named("ListAllTarifsToPdf");
            this.Bind<ICommand>().To<ListAllTarifsByTypeCommand>().Named("ListAllTarifsByType");
            this.Bind<ICommand>().To<RemoveTarifCommand>().Named("RemoveTarif");
            this.Bind<ICommand>().To<UpdateTarifCommand>().Named("UpdateTarif");

            this.Bind<ICommand>().To<CreateLoanCommand>().Named("CreateLoan");
            this.Bind<ICommand>().To<EndLoanCommand>().Named("EndLoan");
            this.Bind<ICommand>().To<ListAllLoansCommand>().Named("ListAllLoans");
            this.Bind<ICommand>().To<ListAllLoansToPdfCommand>().Named("ListAllLoansToPdf");

            var engine = this.Bind<IEngine>().To<Engine>().InSingletonScope();
        }
    }
}