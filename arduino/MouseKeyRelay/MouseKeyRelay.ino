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
      Serial.println(inString);
      if (inChar < 0x400){
        switch (inChar){
          case 128:
          case 129:
          case 130:
            Keyboard.press(inChar);
            break;
          case 131:
          case 132:
          case 133:
            Keyboard.release(inChar - 3);
            break;
          case 640:
            Mouse.release(MOUSE_LEFT);
            break;
          case 641:
            Mouse.click(MOUSE_LEFT);
            break;
          case 642:
            Mouse.press(MOUSE_LEFT);
            break;
          case 768:
            Mouse.release(MOUSE_RIGHT);
            break;
          case 769:
            Mouse.click(MOUSE_RIGHT);
            break;
          case 770:
            Mouse.press(MOUSE_RIGHT);
            break;
          case 896:
            Mouse.release(MOUSE_MIDDLE);
            break;
          case 897:
            Mouse.click(MOUSE_MIDDLE);
            break;
          case 898:
            Mouse.press(MOUSE_MIDDLE);
            break;
          default:
            Keyboard.write(inChar);
            break;
          }
      }
      
      if ((inChar & 0x400) > 0){
          Mouse.move(inChar - 0x400, 0);
          // Mouse.move(10, 0);
      }
      if ((inChar & 0x800) > 0){
          Mouse.move(-1 * (inChar - 0x800), 0);
          // Mouse.move(-10, 0);
      }
      if ((inChar & 0x1000) > 0){
          Mouse.move(0, inChar - 0x1000);
          // Mouse.move(0, 10);
      }
      if ((inChar & 0x2000) > 0){
          Mouse.move(0, -1 * (inChar - 0x2000));
          // Mouse.move(0, -10);
      }
  }
}
