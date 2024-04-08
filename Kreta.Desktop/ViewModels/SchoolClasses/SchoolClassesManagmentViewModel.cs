using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kreta.Desktop.ViewModels.Base;
using Kreta.HttpService.Services;
using Kreta.Shared.Dtos;
using Kreta.Shared.Models;
using Kreta.Shared.Models.SwitchTable;
using Kreta.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Kreta.Desktop.ViewModels.SchoolClasses
{
    public partial class SchoolClassesManagmentViewModel : BaseViewModel
    {
        private ISchoolClassService? _schoolClassService;
        private ISubjectService? _subjectService;
        private ISchoolClassSubjectsService? _schoolClassSubjectsService;

        [ObservableProperty] private SchoolClass? _selectedSchoolClass = new();
        [ObservableProperty] private SchoolClassSubjects? _selectedSchoolClassSubjects = new();

        [ObservableProperty] private ObservableCollection<SchoolClass>? _schoolClasses = new();

        [ObservableProperty] private ObservableCollection<Subject>? _subjectWhoNotStudySchoolClass = new();
        [ObservableProperty] private Subject? _selectedSubjectWhoNotStudySchoolClass = new();

        public SchoolClassesManagmentViewModel()
        {
        }
        public SchoolClassesManagmentViewModel(ISchoolClassService schoolClassService,
                                               ISubjectService subjectService,
                                               ISchoolClassSubjectsService schoolClassSubjectsService)
        {
            _schoolClassService = schoolClassService;
            _subjectService = subjectService;
            _schoolClassSubjectsService = schoolClassSubjectsService;
        }

        public string Title { get; set; } = "Osztályok kezelése";

        public override async Task InitializeAsync()
        {
            await UpdateView();
            await base.InitializeAsync();
        }

        [RelayCommand]
        private async Task GetSubjectNotStudiedInTheSchoolClass()
        {
            await UpdateSubjectNotStudiedInTheSchoolClass();
        }

        private async Task UpdateView()
        {
            await UpdateSubjectNotStudiedInTheSchoolClass();
            await UpdateSchoolClassWithSubject();
        }

        private async Task UpdateSchoolClassWithSubject()
        {
            if (_schoolClassService != null)
            {
                SchoolClass? storedPreviusSchoolClass = SelectedSchoolClass is not null ? SelectedSchoolClass : new();
                List<SchoolClass>? schoolClasses = await _schoolClassService.GetAllSchoolClassWithSubjectsAsync();
                SchoolClasses = new ObservableCollection<SchoolClass>(schoolClasses);
                SelectedSchoolClass = schoolClasses.FirstOrDefault(schoolClass => schoolClass.Id == storedPreviusSchoolClass.Id);
                SelectedSchoolClass = SelectedSchoolClass is not null ? SelectedSchoolClass : new();
            }
        }
        private async Task UpdateSubjectNotStudiedInTheSchoolClass()
        {
            if (_schoolClassService is not null && SelectedSchoolClass is not null && SelectedSchoolClass.HasId)
            {
                List<Subject>? subjects = await _schoolClassService.GetSubjectNotStudiedInTheSchoolClass(SelectedSchoolClass.Id);
                SubjectWhoNotStudySchoolClass = new ObservableCollection<Subject>(subjects);
            }
        }
    }
}
