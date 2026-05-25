using CRMAnalyticsApi.Repositories;
using CRMAnalyticsApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration =
        builder.Configuration.GetConnectionString("Redis");

    options.InstanceName = "CRMAnalytics_";
});

builder.Services.AddScoped<CRMRepository>();

builder.Services.AddScoped<ICRMAnalyticsService,
    CRMAnalyticsService>();

var app = builder.Build();

app.UseAuthorization();

app.MapControllers();

app.Run();


/*

Learning Strategy

Every day has:

Concept Learning
Hands-on Practice
Mini Task/Project
Quiz Questions
Accountability Check



format :
Learn
functions
parameters
return values
Practice
def add(a, b):
    return a + b
Mini Project

Build:

calculator app
Quiz
Why use functions?
What is return?


Millions of people are canceling their subscriptions today.

Here are 08 FREE AI prompts that replace $50,000 worth of courses.

Universities won't teach you this.

1/ Most people open Claude and ask:

"Explain machine learning."

Wrong.

Try this instead:
➡️ "You are my personal ML tutor. I'm a complete beginner. Create a 30-day roadmap with daily tasks, projects, and quizzes. Hold me accountable."

That's a $3,000 bootcamp. Free.

2/ Want to learn Copywriting and make real money?

Prompt:
➡️ "Teach me direct response copywriting from scratch. Give me the top 5 frameworks, daily writing drills, and critique my copy like a $500/hr copywriter."

People pay $997 for this course. You just got it free.

3/ Trying to break into Tech with no degree?

Prompt:
➡️ "I want to become a frontend developer in 90 days. Build me a week-by-week plan. Include free resources, projects for my portfolio, and tell me what to avoid."

This is what coding bootcamps charge $15,000 for.

4/ Learning a new language? Forget Duolingo.

Prompt:
➡️ "Be my Spanish tutor. Teach me like a native speaker. Give me 10 words daily, a short dialogue to practice, correct my mistakes, and adjust to my level as we go."

Real tutors charge $40/hour. Claude does it free, 24/7.

5/ Want to start a business but don't know how?

Prompt:
➡️ "Act as a startup mentor. I want to launch [your idea]. Help me validate the idea, identify my target customer, write a simple business plan, and find my first 10 customers."

That's a $20,000 MBA lesson.

6/ Data Science feels impossible without a degree?

Prompt:
➡️ "I know basic math. Teach me Data Science in 30 days. Cover Python, Pandas, data visualization, and give me one real dataset project per week to build my portfolio."

Coursera charges $399/year for less.

7/ Struggling with productivity and focus?

Prompt:
➡️ "Design a personalized productivity system for me. I work from home, get distracted easily, and have 3 big goals this year. Build my daily schedule, habit tracker, and weekly review system."




*/