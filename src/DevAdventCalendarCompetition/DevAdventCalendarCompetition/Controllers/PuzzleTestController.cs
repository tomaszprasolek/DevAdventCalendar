using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using DevAdventCalendarCompetition.Services.Interfaces;

namespace DevAdventCalendarCompetition.Controllers
{
    public class PuzzleTestController : BaseTestController
    {
        private readonly IPuzzleTestService _puzzleTestService;

        public PuzzleTestController(IBaseTestService baseTestService, IPuzzleTestService puzzleTestService) : base(baseTestService)
        {
            _puzzleTestService = puzzleTestService;
            _puzzleTestService = puzzleTestService;
        }

        [HttpGet]
        [CanStartTest(TestNumber = 7)]
        public IActionResult Index()
        {
            var test = _baseTestService.GetTestByNumber(7);
            return View(test);
        }

        [HttpGet]
        public IActionResult CheckGameStarted()
        {
            var testAnswer =_puzzleTestService.GetEmptyAnswerForStartedTestByUser(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (testAnswer != null)
                return Ok(true);
            else
                return BadRequest(false);
        }

        [HttpPost]
        [CanStartTest(TestNumber = 7)]
        public IActionResult StartGame()
        {
            var test = _baseTestService.GetTestByNumber(7);

            if (test != null)
            {
                _puzzleTestService.SaveEmptyTestAnswer(test.Id, User.FindFirstValue(ClaimTypes.NameIdentifier));
                return Ok("Test started!");            
            }

            return BadRequest();
        }
    
        // TODO: Secure the game from changing values in view and possible winning the game without any effort.

        [HttpPost]
        public IActionResult UpdateGameResult(int moveCount, int gameDuration)
        {
            var testAnswer = _puzzleTestService.GetEmptyAnswerForStartedTestByUser(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (testAnswer != null)
            {
                _puzzleTestService.UpdatePuzzleTestAnswer(testAnswer, moveCount, gameDuration);

                return Ok();
            }

            return BadRequest();
        }

        [HttpPost]
        public IActionResult ResetGame(int moveCount, int gameDuration)
        {
            var testAnswer = _puzzleTestService.GetEmptyAnswerForStartedTestByUser(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (testAnswer != null)
            {
                _puzzleTestService.UpdatePuzzleTestAnswer(testAnswer, moveCount, gameDuration);

                return Ok();
            }

            return BadRequest();
        }
    }
}