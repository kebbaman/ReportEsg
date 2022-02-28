using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReportEsg.Models
{
    public class OrganizationDetailsSurveyResultEntry
    {
        [Display(Name = "Codice")]
        public string Name { get; set; }

        [Display(Name = "Domanda")]
        public string Question { get; set; }

        [Display(Name = "Risposta")]
        public string Answer { get; set; }

        public static List<OrganizationDetailsSurveyResultEntry> GetSurveyResultEntries(OrganizationDetailsSurveySession survey)
        {
            List<OrganizationDetailsSurveyResultEntry> surveyResultEntries = new List<OrganizationDetailsSurveyResultEntry>();

            foreach (SurveyQuestion question in survey.OrganizationDetailsSurvey.OrganizationDetailsSurveyQuestions)
            {
                OrganizationDetailsSurveyResultEntry surveyResultEntry = new OrganizationDetailsSurveyResultEntry();
                surveyResultEntry.Name = question.Name;
                surveyResultEntry.Question = question.Title;
                try
                {
                    surveyResultEntry.Answer = (string)survey.SurveyResultObject[question.Name];
                }
                catch (Exception)
                {
                    continue;
                }
                surveyResultEntries.Add(surveyResultEntry);
            }

            return surveyResultEntries;

        }
    }
}
