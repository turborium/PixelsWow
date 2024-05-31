unit UnitMain;

{$mode delphi}

interface

uses
  Classes, SysUtils, Forms, Controls, Graphics, Dialogs, ExtCtrls, BitmapPixels;

type

  { TFormMain }

  TFormMain = class(TForm)
    PaintBox: TPaintBox;
    Timer: TTimer;
    procedure PaintBoxPaint(Sender: TObject);
    procedure TimerTimer(Sender: TObject);
  private
    Time: Double;
    procedure Draw(Data: TBitmapData);
  public

  end;

var
  FormMain: TFormMain;

implementation

{$R *.lfm}

{ TFormMain }

procedure TFormMain.PaintBoxPaint(Sender: TObject);
var
  Bitmap: TBitmap;
  Data: TBitmapData;
begin
  Bitmap := TBitmap.Create();
  try
    Bitmap.SetSize(PaintBox.Width, PaintBox.Height);
    Data.Map(Bitmap, TAccessMode.Write, False, clBlack);
    try

     Draw(Data);

    finally
      Data.Unmap();
    end;

    PaintBox.Canvas.Draw(0, 0, Bitmap);
  finally
    Bitmap.Free();
  end;
end;

procedure TFormMain.TimerTimer(Sender: TObject);
begin
  Time := Time + 0.0003 * 0.5;
  PaintBox.Invalidate();
end;

procedure TFormMain.Draw(Data: TBitmapData);
var
  I, J: Integer;
  X, Y: Double;
  Color: Integer;
begin
  RandSeed := 1;
  for I := 1 to 100 do
  begin
    X := Random();
    Y := Random();
    Color := Random($00FFFFFF + 1);

    for J := 1 to 1000 do
    begin
      Data.SetPixel(Trunc(X * Data.Width), Trunc(Y * Data.Height), Color);

      // X := Frac(X + Cos(Y));
      // Y := Frac(Y + Sin(X));

      //X := Frac(X + Cos(Y + Time));
      //Y := Frac(Y + Sin(X));

      // X := Frac(X + Cos(Y + Time));
      // Y := Frac(Y + Sin(X + Y * 0.006));

      //X := Frac(X + Cos(Y+X*0.02+Time));
      //Y := Frac(Y + Sin(X*0.5));

      //X := Frac(X + Cos(Y + X * 0.1 + Time));
      //Y := Frac(Y + Sin(X * 0.5));

      //X := Frac(X + Cos(Y + X * 0.1 + Time));
      //Y := Frac(Y + Sin(X * 1.5));

      //X := Frac(X + Cos(X * 0.03 + Y * 1.4 + Time * 1.3));
      //Y := Frac(Y + Sin(Y * 0.07 + X * 1.5 - Time * 3));

      X := Frac(Time + X + Cos(Y + X * 0.1));
      Y := Frac(Time * 0.3 + Y + Sin(X * 1.5));
    end;
  end;
end;

end.

