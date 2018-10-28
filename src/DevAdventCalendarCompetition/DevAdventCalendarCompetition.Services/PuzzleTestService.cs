﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using AutoMapper;
using DevAdventCalendarCompetition.Repository.Interfaces;
using DevAdventCalendarCompetition.Repository.Models;
using DevAdventCalendarCompetition.Services.Interfaces;
using DevAdventCalendarCompetition.Services.Models;

namespace DevAdventCalendarCompetition.Services
{
    public class PuzzleTestService : IPuzzleTestService
    {
        private readonly IPuzzleTestRepository _puzzleTestRepository;
        private readonly IBaseTestRepository _baseTestRepository;

        public PuzzleTestService(IPuzzleTestRepository puzzleTestRepository, IBaseTestRepository baseTestRepository)
        {
            _puzzleTestRepository = puzzleTestRepository;
            _baseTestRepository = baseTestRepository;
        }

        public TestAnswerDto GetEmptyAnswerForStartedTestByUser(string userId)
        {
            var testAnswer = _puzzleTestRepository.GetEmptyAnswerForStartedTestByUser(userId);
            var testAnswerDto = Mapper.Map<TestAnswerDto>(testAnswer);
            return testAnswerDto;
        }

        public void SaveEmptyTestAnswer(int testId, string userId)
        {
            var testAnswer = new TestAnswer
            {
                Answer = null,
                TestId = testId,
                UserId = userId,
                AnsweringTime = DateTime.MinValue,
                AnsweringTimeOffset = TimeSpan.Zero
            };

            _baseTestRepository.AddAnswer(testAnswer);
        }

        public void UpdatePuzzleTestAnswer(TestAnswerDto testAnswerDto, int movesCount, int gameDuration)
        {
            var testAnswer = _puzzleTestRepository.GetEmptyAnswerForStartedTestByUser(testAnswerDto.UserId);
            testAnswer.Answer = movesCount.ToString();
            testAnswer.AnsweringTime = DateTime.Now;
            testAnswer.AnsweringTimeOffset = TimeSpan.FromSeconds(gameDuration);

            _baseTestRepository.UpdateAnswer(testAnswer);

        }
    }
}
