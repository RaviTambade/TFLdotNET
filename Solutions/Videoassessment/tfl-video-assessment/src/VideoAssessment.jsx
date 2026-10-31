
import React, { useState } from "react";
import "./VideoAssessment.css";

const assessmentCriteria = impoty ("./assessmentcriteria.json");

const initialStudent = {
  studentName: "",
  topic: "",
  mentor: "",
  assessmentDate: "",
  feedback: "",
};

function getReadiness(total) {
  if (total >= 90) {
    return {title: "Mentor-ready", description: "Strong independent explanation, demonstration and communication.",};
  }

  if (total >= 75) {
    return {title: "Workplace-ready potential", description: "Good foundation with a few areas to strengthen.", };  }

  if (total >= 60) {
    return { title: "Developing", description:"More practice needed in applying and explaining concepts.", };
  }

  if (total >= 40) {
    return {
      title: "Needs guided practice", description:"Targeted support needed in understanding or communication.",};
  }

  return {
    title: "Rebuild fundamentals", description: "Revisit the concept and submit a fresh demonstration.",};
}

export default  VideoAssessment=()=> {
  //state
  const [student, setStudent] = useState(initialStudent);
  const [scores, setScores] = useState(assessmentCriteria.map(() => 0));
  const [message, setMessage] = useState("");

  const total = scores.reduce((sum, score) => sum + score, 0);
  const readiness = getReadiness(total);

  function updateStudent(event) {
    const { name, value } = event.target;
    setStudent((previous) => ({ ...previous,[name]: value,}));
  }

  function updateScore(index, value) {
    const numericValue = Number(value);

    setScores((previous) => previous.map((score, i) => i === index ? numericValue : score ) );
    setMessage("");
  }

  function buildReport() {
    return [
      "TRANSFLOWER LEARNING — PYTHON MENTOR CHALLENGE", "VIDEO-BASED EMPLOYABILITY ASSESSMENT", "",
      `Student: ${student.studentName.trim() || "Not entered"}`,
      `Python topic: ${student.topic.trim() || "Not entered"}`,
      `Evaluating mentor: ${student.mentor.trim() || "Not entered"}`,
      `Assessment date: ${student.assessmentDate || "Not entered"}`,
      "",
      ...assessmentCriteria.map( (criterion, index) =>`${index + 1}. ${criterion.title}: ${scores[index]}/10`),
      "",
      `TOTAL: ${total}/100`,
      `READINESS LEVEL: ${readiness.title}`,
      `Interpretation: ${readiness.description}`,
      "",
      "MENTOR FEEDBACK:",
      student.feedback.trim() || "Not entered",
      "",
      "Note: This is one source of employability evidence, not a standalone hiring decision.",
    ].join("\n");
  }

  async function copyReport() {
    const report = buildReport();

    try {
      await navigator.clipboard.writeText(report);
      setMessage("Assessment report copied successfully.");
    } catch {
      // Fallback for browsers that block clipboard access.
      const textArea = document.createElement("textarea");
      textArea.value = report;
      textArea.setAttribute("readonly", "");
      textArea.style.position = "fixed";
      textArea.style.left = "-9999px";

      document.body.appendChild(textArea);
      textArea.select();

      const copied = document.execCommand("copy");
      textArea.remove();

      setMessage( copied ? "Assessment report copied successfully." : "Copy was blocked. Use Print / Save as PDF instead." );
    }
  }

  function resetForm() {
    const confirmed = window.confirm("Clear all student details, scores and feedback?" );

    if (!confirmed) return;

    setStudent(initialStudent);
    setScores(assessmentCriteria.map(() => 0));
    setMessage("");
  }

  return (
    <div className="video-assessment">
      <header className="assessment-header">
        <p className="brand">TRANSFLOWER LEARNING</p>
        <h1>Content <Creation></Creation> Challenge</h1>
        <p>Employability Assessment · 20-Minute Presentation</p>
      </header>

      <main className="assessment-main">
        {/* Student Information */}
        <section className="assessment-card">
          <h2>Student Information</h2>
          <p className="intro"> Record the student details before evaluating the video. </p>

          <div className="assessment-fields">
            <div>
              <label htmlFor="studentName">Student name / ID</label>
              <input
                id="studentName"
                name="studentName"
                value={student.studentName}
                onChange={updateStudent}
                placeholder="Enter student name or ID"
              />
            </div>

            <div>
              <label htmlFor="topic">Python topic</label>
              <input
                id="topic"
                name="topic"
                value={student.topic}
                onChange={updateStudent}
                placeholder="e.g. Decorators, Generators, Exceptions"
              />
            </div>

            <div>
              <label htmlFor="mentor">Evaluating mentor</label>
              <input
                id="mentor"
                name="mentor"
                value={student.mentor}
                onChange={updateStudent}
                placeholder="Mentor name"
              />
            </div>

            <div>
              <label htmlFor="assessmentDate">Assessment date</label>
              <input
                id="assessmentDate"
                name="assessmentDate"
                type="date"
                value={student.assessmentDate}
                onChange={updateStudent}
              />
            </div>
          </div>
        </section>

        {/* Assessment Rubric */}
        <section className="assessment-card">
          <h2>Assessment Rubric — 100 Marks</h2>
          <p className="intro"> Score each parameter from 0 to 10. Evaluate observable
            evidence, not just video polish.
          </p>

          {assessmentCriteria.map((criterion, index) => (
            <article className="criterion" key={criterion.title}>
              <div className="criterion-top">
                <div>
                  <h3>
                    {index + 1}. {criterion.title}
                  </h3>
                  <p>{criterion.detail}</p>
                </div>

                <div className="score">
                  {scores[index]} / 10
                </div>
              </div>

              <label className="visually-hidden" htmlFor={`score-${index}`} >{criterion.title}, score out of 10</label>

              <input id={`score-${index}`}
                className="score-range"
                type="range"
                min="0"
                max="10"
                step="1"
                value={scores[index]}
                onChange={(event) =>updateScore(index, event.target.value)}
              />

              <div className="range-labels">
                <span>0 — Insufficient</span>
                <span>5 — Developing</span>
                <span>10 — Excellent</span>
              </div>
            </article>
          ))}
        </section>

        {/* Overall Result */}
        <section className="assessment-card">
          <h2>Overall Result</h2>

          <div className="assessment-summary" aria-live="polite">
            <div>
              <small>TOTAL SCORE</small>
              <div className="total-score">
                {total}
                <span> / 100</span>
              </div>
            </div>

            <div>
              <div className="readiness-level">
                {readiness.title}
              </div>
              <p className="readiness-description">
                {readiness.description}
              </p>
            </div>
          </div>

          <div className="feedback-field">
            <label htmlFor="feedback">
              Mentor feedback — evidence, strengths and next steps
            </label>
            <textarea
              id="feedback"
              name="feedback"
              rows="5"
              value={student.feedback}
              onChange={updateStudent}
              placeholder="Example: Explains loops clearly. Next step: demonstrate a boundary case and explain why it occurs."
            />
          </div>

          <div className="assessment-actions">
            <button
              type="button"
              className="primary-button"
              onClick={copyReport}
            >
              Copy Assessment Report
            </button>

            <button type="button" className="secondary-button" 
                    onClick={() => {
                        setMessage("");
                        window.print();
                    }}> 
                    Print / Save as PDF
            </button>

            <button type="button"
                    className="neutral-button"
                    onClick={resetForm}
            >
              Reset Form
            </button>
          </div>

          <p className="assessment-message" role="status">
            {message}
          </p>
        </section>

        {/* Scoring Guide */}
        <section className="assessment-card">
          <h2>Scoring Guide</h2>

          <div className="scoring-guide">
            <div>
              <strong>9–10 · Excellent</strong>
              Independent explanation, strong demonstration and real-world connection.
            </div>
            <div>
              <strong>7–8 · Good</strong>
              Correct and clear, with minor gaps or occasional reliance
              on notes.
            </div>
            <div>
              <strong>5–6 · Developing</strong>
              Understands basics but needs more depth, clarity or    troubleshooting.
            </div>
            <div>
              <strong>3–4 · Weak</strong>
              Mostly memorized; limited understanding or incomplete  demonstration.
            </div>
            <div>
              <strong>0–2 · Insufficient evidence</strong>
              Cannot explain the core idea or provide meaningful  evidence.
            </div>
          </div>

          <p className="intro scoring-note">
            Suggested readiness bands: 90–100 Mentor-ready · 75–89
            Workplace-ready potential · 60–74 Developing · 40–59 Needs
            guided practice · 0–39 Rebuild fundamentals. These are
            internal guidance bands, not universal hiring standards.
          </p>
        </section>
      </main>

      <footer className="assessment-footer"> Transflower Learning · Learn deeply. Demonstrate clearly.  Mentor responsibly.</footer>
    </div>
  );
}