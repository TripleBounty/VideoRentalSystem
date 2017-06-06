#Video Rental Store

<img src="https://vignette2.wikia.nocookie.net/tinytower/images/b/bd/Video_Rental.png/revision/latest?cb=20130515011338"
alt="IMAGE ALT TEXT HERE" width="480" height="140" border="10" /><br>

Commands separator ";" <br>

1.0. Commands <br>
<ul>
<li>1.1. Create Commands <br><br>
--CreateCountry - params - Initials, Country Code.<br>
--CreateTown - params - TownName, CountryId.<br>
--CreateAddress - params - Street, PostalCode, TownID.<br>
--CreateEmployee - params - First Name, Last Name, Salary, Manager Id.<br>
--CreateManager - params - First Name, Last Name, Salary.<br>
--CreateCustomer - params - First Name, Last Name, Birthdate MM/dd/YYYY.<br>
--CreateFilm - params - Film Name, Summary, Release date MM/dd/YYYY.<br>
--CreateFilmGenre -  params - Genre.<br>
--CreateFilmRating - params - Rating.<br>
--CreateFilmStaff - params - First Name, Last Name, Birthdate MM/dd/YYYY, CountryId, Type.<br>
--CreateStore - params - StoreName, AddressId.<br>
--CreateStorage - params - StoreName, AddressId.<br>
<br><br>
</li>
<li>1.2. List Commands<br><br>
--ListAllCountries - no params<br>
--CountryDetails - params - CountryId<br>
--ListAllTowns - no params <br>
--TownDetails - params - TownId <br>
--ListAllAddresses - no params <br>
--AddressDetails - params - AddressId <br>
--ListAllEmployees - no params <br>
--ListAllCustomers - no params <br>
--ListAllStores - no params <br>
--StoreDetails - params - StoreId <br>
--ListAllStorages - no params <br>
--StoragesDetails - params - StoreId <br>
<br><br>
</li>
<li>1.3. Update Commands<br><br>
--UpdateCountry - params - CountryId, Initials, Country Code.<br>
--UpdateTown - params - TownId, TownName, CountryId.<br>
--UpdateAddress - params - AddressId, Street, PostalCode, TownID.<br>
<br><br>
</li>
<li>1.4. Add Commands<br><br>
--AddAwardToFilm - params - FilmName, Award.<br>
--AddGenreToFilm - params - FilmName, Genre.<br>
--AddRatingToFilm - params - FilmName, Rating.<br>
--AddStoreEmployee - params - StoreId, EmployeeId.<br>
--AddFilmQuantityCommand - params - StorageId, Qunatity.<br>
<br><br>
</li>
<li>1.5. Remove Commands<br><br>
--RemoveStoreEmployee - params - StoreId, EmployeeId.<br>
--RemoveFilmQuantityCommand - params - StorageId, Qunatity.<br>
<br><br>
</li>
<li>1.6. Termination<br><br>
--Exit<br>
</li>
<br><br>
<li>1.7. Enums<br><br>
  <ul>
  <li>1.7.1 StaffType<br>
        Actor<br>
        Director<br>
        Writer<br>
  </li>
  <li>1.7.4 VideoFormat<br>
      CD <br>
      DVD <br>
      HDDVD <br>
      BluRay <br>
      BluRay3D <br>
      BluRay4K <br>
      VHS <br>
  </li>
  </ul>
</li>
<br><br>
</ul>
<br>
<br>