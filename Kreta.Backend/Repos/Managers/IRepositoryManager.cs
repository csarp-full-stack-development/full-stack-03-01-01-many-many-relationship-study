﻿namespace Kreta.Backend.Repos.Managers
{
    public interface IRepositoryManager
    {
        public ISchoolClassRepo? SchoolClassRepo { get; }
        public ISubjectRepo? SubjectRepo { get; }
    }
}
