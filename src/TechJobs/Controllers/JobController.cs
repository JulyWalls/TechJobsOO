using Microsoft.AspNetCore.Mvc;
using TechJobs.Data;
using TechJobs.ViewModels;
using TechJobs.Models;

namespace TechJobs.Controllers
{
    public class JobController : Controller
    {

        // Our reference to the data store
        private static JobData jobData;

        static JobController()
        {
            jobData = JobData.GetInstance();
        }

        // The detail display for a given Job at URLs like /Job?id=17
        public IActionResult Index(int id)
        {
            Job singleJob = jobData.Find(id);

            // TODO #1 - get the Job with the given ID and pass it into the view

            return View(singleJob);
        }

        public IActionResult New()
        {
            NewJobViewModel newJobViewModel = new NewJobViewModel();
            return View(newJobViewModel);
        }

        [HttpPost]
        public IActionResult New(NewJobViewModel newJobViewModel)
        {
            Employer anEmployer = jobData.Employers.Find(newJobViewModel.EmployerID);
            Location aLocation = jobData.Locations.Find(newJobViewModel.LocationsID);
            CoreCompetency aCompetency = jobData.CoreCompetencies.Find(newJobViewModel.CompetencyID);
            PositionType aPositionType = jobData.PositionTypes.Find(newJobViewModel.PositionTypeID);


            if (ModelState.IsValid)
            {
                Job newJob = new Job
                {
                    Name = newJobViewModel.Name,
                    Employer = anEmployer,
                    Location = aLocation,
                    CoreCompetency = aCompetency,
                    PositionType = aPositionType
                };

                jobData.Jobs.Add(newJob);

                return Redirect("/Job?id=" + newJob.ID );
            }
            // TODO #6 - Validate the ViewModel and if valid, create a 
            // new Job and add it to the JobData data store. Then
            // redirect to the Job detail (Index) action/view for the new Job.

            return View(newJobViewModel);
        }
    }
}
