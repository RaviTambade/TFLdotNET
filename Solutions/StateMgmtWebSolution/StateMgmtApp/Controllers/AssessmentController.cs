using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;

namespace StateMgmtApp.Controllers
{
    public class AssessmentController : Controller
    {
        public IActionResult Index()
        {
            //Available tests
            var tests = new List<string>
            {
                "Math Test",
                "Science Test",
                "History Test"
            };
            ViewBag.tests = tests;

            return View();
        }
        public IActionResult StartTest(int id)
        {
            //Take a test
            //id is the test id
            //Load questions based on the test id
            DateTime startTime = DateTime.Now;
            //Store the start time in session or database for later use

            HttpContext.Session.SetString("TestStartTime", startTime.ToString());

            return View();
        }

        public IActionResult Question(int id)
        {
            //Load question based on the question id
            //id is the question id
            return View();
        }

        public IActionResult SubmitAnswer(int questionId, string answer)
        {
            //Submit the answer for the question
            //questionId is the question id
            //answer is the answer provided by the user
            //Process the answer and return the result
            return RedirectToAction("Result");
        }

        public IActionResult SubmitTest(int testId)
        {
            //Submit the test answers
            DateTime StartTime =DateTime.Parse(HttpContext.Session.GetString("TestStartTime"));
            DateTime EndTime = DateTime.Now;
            //Calculate the total time taken for the test
            TimeSpan duration = EndTime -StartTime; // Assuming you have the start time stored

            return RedirectToAction("TestSummary", new { testId = testId , duration=duration});

        }

        public IActionResult TestSummary()
        {
            //Display the result of the test

            var result = new
            {
                Score = 85,
                TotalQuestions = 10,
                CorrectAnswers = 8,
                IncorrectAnswers = 2
            };
            return View(result);
        }
    }
}
