using CalorieTracker.BLL.Interfaces;
using CalorieTracker.DAL.DTO.Response;
using CalorieTracker.DAL.Models;
using CalorieTracker.DAL.Repositories.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CalorieTracker.BLL.Service
{
    public class ReportService : IReportService
    {
        private readonly IMealRepository _mealRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReportService(IMealRepository mealRepository,UserManager<ApplicationUser> userManager)
        {
            _mealRepository = mealRepository;
            _userManager = userManager;
        }

        public async Task<BaseResponse<DailyReportResponse>> GetDailyReportAsync(string userId, DateTime date)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user is null)
                    return new BaseResponse<DailyReportResponse>
                    {
                        Success = false,
                        Message = "User not found"
                    };

                var meals = await _mealRepository.Query()
                    .Where(m => m.UserId == userId &&
                                m.Date.Date == date.Date)
                    .ToListAsync();

                var totalCalories = meals
                    .SelectMany(m => m.MealFoods)
                    .Sum(mf => mf.TotalCalories);

                var totalProtein = meals
                    .SelectMany(m => m.MealFoods)
                    .Sum(mf => mf.TotalProtein);

                var totalFat = meals
                    .SelectMany(m => m.MealFoods)
                    .Sum(mf => mf.TotalFat);

                var totalCarbs = meals
                    .SelectMany(m => m.MealFoods)
                    .Sum(mf => mf.TotalCarbs);

                return new BaseResponse<DailyReportResponse>
                {
                    Success = true,
                    Message = "Daily report retrieved successfully",
                    Data = new DailyReportResponse
                    {
                        Date = date.Date,
                        DailyCalorieGoal = user.DailyCalorieGoal,
                        TotalCaloriesConsumed = totalCalories,
                        RemainingCalories = user.DailyCalorieGoal - totalCalories,
                        TotalProtein = totalProtein,
                        TotalFat = totalFat,
                        TotalCarbs = totalCarbs,
                        Meals = meals.Adapt<List<MealResponse>>()
                    }
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<DailyReportResponse>
                {
                    Success = false,
                    Message = "Failed to get daily report",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<BaseResponse<WeeklyReportResponse>> GetWeeklyReportAsync(string userId, DateTime startDate)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user is null)
                    return new BaseResponse<WeeklyReportResponse>
                    {
                        Success = false,
                        Message = "User not found"
                    };

                var endDate = startDate.AddDays(6);

                var meals = await _mealRepository.Query()
                    .Where(m => m.UserId == userId &&
                                m.Date.Date >= startDate.Date &&
                                m.Date.Date <= endDate.Date)
                    .ToListAsync();

                var dailyReports = new List<DailyReportResponse>();

                for (int i = 0; i < 7; i++)
                {
                    var currentDate = startDate.AddDays(i).Date;
                    var dayMeals = meals
                        .Where(m => m.Date.Date == currentDate)
                        .ToList();

                    var dayCalories = dayMeals
                        .SelectMany(m => m.MealFoods)
                        .Sum(mf => mf.TotalCalories);

                    var dayProtein = dayMeals
                        .SelectMany(m => m.MealFoods)
                        .Sum(mf => mf.TotalProtein);

                    var dayFat = dayMeals
                        .SelectMany(m => m.MealFoods)
                        .Sum(mf => mf.TotalFat);

                    var dayCarbs = dayMeals
                        .SelectMany(m => m.MealFoods)
                        .Sum(mf => mf.TotalCarbs);

                    dailyReports.Add(new DailyReportResponse
                    {
                        Date = currentDate,
                        DailyCalorieGoal = user.DailyCalorieGoal,
                        TotalCaloriesConsumed = dayCalories,
                        RemainingCalories = user.DailyCalorieGoal - dayCalories,
                        TotalProtein = dayProtein,
                        TotalFat = dayFat,
                        TotalCarbs = dayCarbs,
                        Meals = dayMeals.Adapt<List<MealResponse>>()
                    });
                }

                var totalCaloriesWeek = dailyReports.Sum(d => d.TotalCaloriesConsumed);

                return new BaseResponse<WeeklyReportResponse>
                {
                    Success = true,
                    Message = "Weekly report retrieved successfully",
                    Data = new WeeklyReportResponse
                    {
                        StartDate = startDate.Date,
                        EndDate = endDate.Date,
                        DailyCalorieGoal = user.DailyCalorieGoal,
                        TotalCaloriesConsumed = totalCaloriesWeek,
                        AverageCaloriesPerDay = totalCaloriesWeek / 7,
                        DailyReports = dailyReports
                    }
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<WeeklyReportResponse>
                {
                    Success = false,
                    Message = "Failed to get weekly report",
                    Errors = new List<string> { ex.Message }
                };
            }
        }
    }
}