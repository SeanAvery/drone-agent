import math
import numpy as np

class QuadCopter():
    def __init__(self):
        self.position = (0, 0, 0)
        self.velocity = (0, 0, 0)
        self.angle = (0, 0, ) # pitch, yaw, roll
        self.angle_velocity = (0, 0, 0)

class Rotor():
    def __init__(self, motor_torque_constant):
        self.motor_torque_constant = motor_torque_constant
        self.initial_current = 0
        self.current = 0
        Aself.torque = self.calc_torque()

    def calc_torque(self):
        return self.motor_torque_constant * (self.current - self.initial_current)
        
if __name__ == '__main__':
    motor_torque_constant = 1000
    copter = QuadCopter()
