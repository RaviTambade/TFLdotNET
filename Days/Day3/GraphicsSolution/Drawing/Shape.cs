namespace Drawing;

public enum Color {RED, GREEN,  BLUE};
public enum WeekDays{Sunday, Monday, Tuesday};

public abstract class Shape {
    protected int Width{get;set;}
    protected Color Color{get;set;}
    public abstract void Draw();

}