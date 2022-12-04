using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TurboRenting.Front.Enums;
using TurboRenting.Front.HttpClientHelpper.HCClients;
using TurboRenting.Front.HttpClientHelpper.HCContracts;
using TurboRenting.Front.HttpClientHelpper.HCGarages;
using TurboRenting.Front.HttpClientHelpper.HCUsers;
using TurboRenting.Front.HttpClientHelpper.HCVehicules;

namespace TurboRenting.Front.Helpers
{
    public class DataValidators
    {

        Regex validateNumberRegex = new Regex("^(?:-(?:[1-9](?:\\d{0,2}(?:,\\d{3})+|\\d*))|(?:0|(?:[1-9](?:\\d{0,2}(?:,\\d{3})+|\\d*))))(?:.\\d+|)$");
        Regex validateLetterRegex = new Regex("^[a-zA-Z0-9]*$");
        List<string> controlDigits = new List<string> { "T", "R", "W", "A", "G", "M", "Y", "F", "P", "D", "X", "B", "N", "J", "Z", "S", "Q", "V", "H", "L", "C", "K", "E" };
        public bool validateClientInfo(Client createdClient)
        {

            if (!createdClient.LastName.Contains(' '))
            {
                return false;
            }
            else
            {
                string firstLastName = createdClient.LastName.Split(' ')[0];
                string SecondLastName = createdClient.LastName.Split(' ')[1];

                if (validateEmail(createdClient.Email) && !String.IsNullOrEmpty(createdClient.FirstName)
                  && !String.IsNullOrEmpty(createdClient.LastName) && validateLetterRegex.IsMatch(createdClient.FirstName)
                  && validateLastName(firstLastName, SecondLastName) && validateNumberRegex.IsMatch(createdClient.Phone))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private bool validateEmail(string email)
        {
            if (!String.IsNullOrEmpty(email))
            {
                if (email.Contains("@"))
                {
                    var dom = email.Split('.')[1];
                    var posibleDoms = new List<string> {"com", "net", "es", "org", "edu", "int"};
                    if (posibleDoms.Contains(dom))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private bool validateLastName(string lastName1, string lastName2)
        {
            if(validateLetterRegex.IsMatch(lastName1) && validateLetterRegex.IsMatch(lastName2))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool validateCreateClientData(string clientEmail, ObservableCollection<Client> existedClientes)
        {
            foreach (var client in existedClientes)
            {
                if (client.Email.Equals(clientEmail))
                {
                    return false;
                }
            }
            return true;
        }

        public bool validateEditClientData(string clientEmailBU, string clientEmail, ObservableCollection<Client> existedClientes)
        {

            if (clientEmailBU.Equals(clientEmail))
            {
                var actualClient = existedClientes.Where(c => c.Email == clientEmail).SingleOrDefault();

                existedClientes.Remove(actualClient);
            }

            foreach (var client in existedClientes)
            {
                if (client.Email.Equals(clientEmail))
                {
                    return false;
                }
            }
            return true;
        }

        public bool validateDniNiePasport(string dni_nie_pasport)
        {
            var DocType = "";
            var charAux = "";

            if (dni_nie_pasport.Length == 10)
            {
                DocType = DocTypes.PASAPORTE.ToString();
            }
            else
            {
                charAux = dni_nie_pasport.Substring(0, 1);

                if (validateNumberRegex.IsMatch(charAux))
                {
                    DocType = DocTypes.DNI.ToString();
                }
                else
                {
                    DocType = DocTypes.NIE.ToString();
                }
            }
            

            if (DocType.Equals(DocTypes.NIE.ToString()))
            {
                if(validateNIE(dni_nie_pasport, charAux))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if(DocType.Equals(DocTypes.DNI.ToString()))
            {
                if (validateDNI(dni_nie_pasport))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (validatePasport(dni_nie_pasport))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private bool validateNIE(string nie, string firstChar)
        {
            if(nie.Length == 9)
            {
                var lastChar = nie.Substring(0, 8);
                var nieLetters = new List<string> { "X", "Y", "Z" };

                if (nieLetters.Contains(firstChar))
                {
                    if (controlDigits.Contains(lastChar) && digitControlIsValid(nie, lastChar, DocTypes.NIE))
                    {
                        var nieNumbers = nie.Substring(1, 6);
                        if (validateNumberRegex.IsMatch(nieNumbers) && validateLetterRegex.IsMatch(firstChar) && validateLetterRegex.IsMatch(lastChar))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private bool validateDNI(string dni)
        {
            if(dni.Length == 9)
            {
                var dniNumbers = dni.Substring(0, 8);
                var dniLetter = dni.Substring(8);
                if(validateNumberRegex.IsMatch(dniNumbers) && validateLetterRegex.IsMatch(dniLetter))
                {
                    if (controlDigits.Contains(dniLetter) && digitControlIsValid(dni, dniLetter, DocTypes.DNI))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private bool validatePasport(string pasport)
        {
            var pasportLetters = pasport.Substring(0, 3);
            var pasportNumbers = pasport.Substring(3, 6);
            var controlDigit = pasport.Substring(9);
            if(validateLetterRegex.IsMatch(pasportLetters) && validateNumberRegex.IsMatch(pasportNumbers))
            {
                if (controlDigits.Contains(controlDigit))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private bool digitControlIsValid(string doc, string digit, DocTypes type)
        {
            if(type == DocTypes.DNI)
            {
                var dniNumbers = doc.Substring(0, 8);
                var numbers = int.Parse(dniNumbers);

                var mod = numbers % 23;

                if (digit.Equals(controlDigits[mod]))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if(type == DocTypes.NIE)
            {
                var nieNumbers = doc.Substring(0, 8);
                var firstChar = nieNumbers[0];

                if (firstChar.Equals("X"))
                {
                    nieNumbers.Replace("X", "0");
                    var numbers = int.Parse(nieNumbers);
                    var mod = numbers % 23;

                    if (digit.Equals(controlDigits[mod]))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                else if (firstChar.Equals("Y"))
                {
                    nieNumbers.Replace("Y", "1");
                    var numbers = int.Parse(nieNumbers);
                    var mod = numbers % 23;

                    if (digit.Equals(controlDigits[mod]))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                else if (firstChar.Equals("Z"))
                {
                    nieNumbers.Replace("Z", "2");
                    var numbers = int.Parse(nieNumbers);
                    var mod = numbers % 23;

                    if (digit.Equals(controlDigits[mod]))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool validateUserInfo(User createdUser)
        {
            if (validateEmail(createdUser.Email) && !String.IsNullOrEmpty(createdUser.Name)
            && !String.IsNullOrEmpty(createdUser.Password) && validateLetterRegex.IsMatch(createdUser.Name))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool validateEditUserData(string userEmailBU , string userEmail, ObservableCollection<User> existedUsers)
        {
            if (userEmailBU.Equals(userEmail))
            {
                var actualUser = existedUsers.Where(u => u.Email == userEmail).SingleOrDefault();

                existedUsers.Remove(actualUser);
            }

            foreach (var user in existedUsers)
            {
                if (user.Email.Equals(userEmail))
                {
                    return false;
                }
            }

            return true;
        }

        public bool validateCreateUserData(string userEmail, ObservableCollection<User> existedUsers)
        {
            foreach (var user in existedUsers)
            {
                if (user.Email.Equals(userEmail))
                {
                    return false;
                }
            }

            return true;
        }

        public bool validateIsNumber(string number)
        {
            if (validateNumberRegex.IsMatch(number))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool validateGarageInfo(Garage createdGarage)
        {
            if (!String.IsNullOrEmpty(createdGarage.Name) && !String.IsNullOrEmpty(createdGarage.Address)
            && !String.IsNullOrEmpty(createdGarage.Location) && !String.IsNullOrEmpty(createdGarage.Phone)
            && validateLetterRegex.IsMatch(createdGarage.Location) && validateNumberRegex.IsMatch(createdGarage.Phone)
            && isValidNumber(createdGarage.Capacity))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool isValidNumber(int capacity)
        {
            if(capacity > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool validateEditGarageData(string garageNameBU, string garageName, ObservableCollection<Garage> existedGarages)
        {
            if (garageNameBU.Equals(garageName))
            {
                var actualGarage = existedGarages.Where(u => u.Name == garageName).SingleOrDefault();

                existedGarages.Remove(actualGarage);
            }
            
            foreach (var user in existedGarages)
            {
                if (user.Name.Equals(garageName))
                {
                    return false;
                }
            }

            return true;
        }

        public bool validateCreateGarageData(string garageName, ObservableCollection<Garage> existedGarages)
        {

            foreach (var garage in existedGarages)
            {
                if (garage.Name.Equals(garageName))
                {
                    return false;
                }
            }

            return true;
        }

        public ObservableCollection<Garage> ValidateGarageCapacity(ObservableCollection<Garage> GarageList)
        {
            ObservableCollection<Garage> garageListAux = new ObservableCollection<Garage>();

            foreach(var Garage in GarageList)
            {

                if (Garage.Vehicules.Count() < Garage.Capacity)
                {
                    garageListAux.Add(Garage);
                }                
            }

            return garageListAux;
        }

        public bool validateVehiculeInfo(Vehicule createdVehicule)
        {
            if (!String.IsNullOrEmpty(createdVehicule.Registration) && !String.IsNullOrEmpty(createdVehicule.Brand) 
                && !String.IsNullOrEmpty(createdVehicule.Model) && !String.IsNullOrEmpty(createdVehicule.Registration)
                && !String.IsNullOrEmpty(createdVehicule.Image) && createdVehicule.Fuel != null && validateNumberData(createdVehicule))
            {
                if (createdVehicule.Registration.Length <= 9 ) 
                {
                    if (validateLetterRegex.IsMatch(createdVehicule.Brand))
                    {
                        if (validateLogicalData(createdVehicule))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }

            }
            else
            {
                return false;
            }
        }

        private bool validateLogicalData(Vehicule createdVehicule)
        {

            var registration = String.Concat(createdVehicule.Registration.Where(v => !Char.IsWhiteSpace(v)));
            var brand = String.Concat(createdVehicule.Brand.Where(v => !Char.IsWhiteSpace(v)));
            var model = String.Concat(createdVehicule.Model.Where(v => !Char.IsWhiteSpace(v)));

            if (validateLetterRegex.IsMatch(registration)
                && validateLetterRegex.IsMatch(brand)
                && validateLetterRegex.IsMatch(model))
            {
                if(createdVehicule.Cv > 0 && createdVehicule.Cv < 1000)
                {
                    if(createdVehicule.NDoors >= 3 && createdVehicule.NDoors <= 5)
                    {
                        if(createdVehicule.WheelsSize >= 15 && createdVehicule.WheelsSize <= 21)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private bool validateNumberData(Vehicule createdVehicule)
        {
            if (createdVehicule.GarageId != 0 && createdVehicule.Cv != 0 
                && createdVehicule.NDoors != 0 && createdVehicule.WheelsSize != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool validateCreateVehiculeData(string vehiculeRegistration, ObservableCollection<Vehicule> existedVehicules)
        {
            foreach (var vehicule in existedVehicules)
            {
                if (vehicule.Registration.Equals(vehiculeRegistration))
                {
                    return false;
                }
            }

            return true;
        }

        public bool validateEditVehiculeData(string vehiculeRegistrationBU, string vehiculeRegistration, ObservableCollection<Vehicule> existedVehicules)
        {
            if (vehiculeRegistrationBU.Equals(vehiculeRegistration))
            {
                var actualVehicule = existedVehicules.Where(v => v.Registration == vehiculeRegistration).SingleOrDefault();

                existedVehicules.Remove(actualVehicule);
            }

            foreach (var vehicule in existedVehicules)
            {
                if (vehicule.Registration.Equals(vehiculeRegistration))
                {
                    return false;
                }
            }

            return true;
        }

        public bool validateContractInfo(Contract CreatedContract, ObservableCollection<Contract> existedContracts)
        {
            if (CreatedContract.TypeName != null)
            {
                if (CreatedContract.BeginingDate != null && CreatedContract.EndingDate != null)
                {
                    var contractCode = String.Concat(CreatedContract.ContractCode.Where(cr => !Char.IsWhiteSpace(cr)));
                    if (!String.IsNullOrEmpty(contractCode) && validateLetterRegex.IsMatch(contractCode))
                    {
                        foreach(var contract in existedContracts)
                        {
                            if (contract.ContractCode.Equals(contractCode))
                            {
                                return false;
                            }
                        }
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

    }
}
