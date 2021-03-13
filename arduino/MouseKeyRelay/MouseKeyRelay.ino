/*
  KeyboardAndMouseControl

  Controls the mouse from five pushbuttons on an Arduino Leonardo, Micro or Due.

  Hardware:
  - five pushbuttons attached to D2, D3, D4, D5, D6

  The mouse movement is always relative. This sketch reads four pushbuttons, and
  uses them to set the movement of the mouse.

  WARNING: When you use the Mouse.move() command, the Arduino takes over your
  mouse! Make sure you have control before you use the mouse commands.

  created 15 Mar 2012
  modified 27 Mar 2012
  by Tom Igoe

  This example code is in the public domain.

  http://www.arduino.cc/en/Tutorial/KeyboardAndMouseControl
*/

#include "Keyboard.h"
#include "Mouse.h"
#include "SoftwareSerial.h"

SoftwareSerial mySerial(8, 3);

void setup() { // initialize the buttons' inputs:

  Serial.begin(9600);
  // initialize mouse control:
  Mouse.begin();
  Keyboard.begin();
  /*
  while (!Serial) {
    ; // シリアルポートの準備ができるのを待つ(Leonardoのみ必要)
  }
  */
  mySerial.begin(9600);
  Keyboard.releaseAll();
}

void loop() {
  // use serial input to control the mouse:
  if(mySerial.available() > 0) {
      String inString = mySerial.readStringUntil(';');
      // mySerial.writeString(inChar);
      int inChar = inString.toInt();
      // Serial.println(inString);
      if (inChar < 0x260){
        switch (inChar){
          case 128:
          case 129:
          case 130:
            Keyboard.press(inChar);
            return;
          case 131:
          case 132:
          case 133:
            Keyboard.release(inChar - 3);
            return;
          case 512:
            Mouse.release(MOUSE_LEFT);
            return;
          case 513:
            Mouse.click(MOUSE_LEFT);
            return;
          case 514:
            Mouse.press(MOUSE_LEFT);
            return;
          case 544:
            Mouse.release(MOUSE_RIGHT);
            return;
          case 545:
            Mouse.click(MOUSE_RIGHT);
            return;
          case 546:
            Mouse.press(MOUSE_RIGHT);
            return;
          case 576:
            Mouse.release(MOUSE_MIDDLE);
            return;
          case 577:
            Mouse.click(MOUSE_MIDDLE);
            return;
          case 578:
            Mouse.press(MOUSE_MIDDLE);
            return;
          default:
            Keyboard.write(inChar);
            return;
          }
      } else if (inChar < 0x270) {
          Mouse.move(0, 0, inChar - 0x260);
      } else if (inChar < 0x400){
          Mouse.move(0, 0, -1 * (inChar - 0x270));
      } else if (inChar < 0x500) {
            Mouse.move(inChar - 0x400, 0);
      } else if (inChar < 0x600){
            Mouse.move(-1 * (inChar - 0x500), 0);
      } else if (inChar < 0x700){
            Mouse.move(0, inChar - 0x600);
      } else if (inChar < 0x800){
            Mouse.move(0, -1 * (inChar - 0x700));
      }
      delay(1);
  }
}
