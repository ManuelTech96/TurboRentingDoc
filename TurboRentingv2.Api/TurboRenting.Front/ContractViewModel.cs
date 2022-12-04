using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboRenting.Front.Enums;
using TurboRenting.Front.HttpClientHelpper.HCClients;
using TurboRenting.Front.HttpClientHelpper.HCContracts;
using TurboRenting.Front.Repositories;

namespace TurboRenting.Front
{
    public class ContractViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Contract> contractList;

        private ContractRepository contractRepo = new ContractRepository();

        private List<ContractTypes> contractTypesList;

        public ContractViewModel()
        {
            GenerateSource();
            LoadContractTypeList();
        }

        public ObservableCollection<Contract> ContractList
        {
            get { return contractList; }
            set
            {
                contractList = value;
                OnPropertyChanged("contractList");
            }
        }

        public List<ContractTypes> ContractTypesList
        {
            get { return contractTypesList; }
            set
            {
                contractTypesList = value;
                OnPropertyChanged("contractTypesList");
            }
        }

        private async void GenerateSource()
        {
            contractList = new ObservableCollection<Contract>();

            await Task.Delay(100);

            await foreach (Contract contract in contractRepo.GetContractList())
            {
                contractList.Add(contract);
            }
        }

        private void LoadContractTypeList()
        {
            contractTypesList = new List<ContractTypes>
            {
                new ContractTypes { TypeName = "Alquiler", ContractType = TypeNames.ALQUILER },
                new ContractTypes { TypeName = "Leasing", ContractType = TypeNames.LEASING },
                new ContractTypes { TypeName = "Renting", ContractType = TypeNames.RENTING },
            };
        }

        public void CreateContract(Contract createdContract)
        {
            contractRepo.PerformCreate(createdContract);
        }

        public void UpdateContract(int contractToUpdateId, Contract updatedContract)
        {
            contractRepo.PerformUpdate(contractToUpdateId, updatedContract);
        }

        public async void DeleteContract(Contract contractToDelete)
        {
            var contractId = contractToDelete.Id;

            contractRepo.PerformDelete(contractId);

            var listContract = new List<Contract>();

            await foreach (var contract in contractRepo.GetContractList())
            {
                listContract.Add(contract);
            }

            contractList.Clear();

            foreach (var contract in listContract)
            {
                contractList.Add(contract);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }

    public class ContractTypes
    {
        public string TypeName { get; set; }
        public TypeNames ContractType { get; set; }
    }
}
