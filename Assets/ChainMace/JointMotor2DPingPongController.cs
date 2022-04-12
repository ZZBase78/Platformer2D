using UnityEngine;

namespace Platformer2D.Assets.ChainMace
{
    internal sealed class JointMotor2DPingPongController
    {
        private const float DEFAULT_MAX_MOTOR_TORQUE = 10000f;
        internal sealed class MotorState
        {
            public JointMotor2D motor2D;
            public float timeToChangeDirection;
            public MotorState nextState;

            public MotorState(float motorSpeed, float timeToChangeDirection)
            {
                motor2D.motorSpeed = motorSpeed;
                motor2D.maxMotorTorque = DEFAULT_MAX_MOTOR_TORQUE;
                this.timeToChangeDirection = timeToChangeDirection;
            }
        }

        private MotorState currentState;
        private float timer;

        public JointMotor2DPingPongController(float motorSpeed, float timeToChangeDirection, float randomStartTime)
        {
            MotorState randomState = new MotorState(0f, Random.Range(0f, randomStartTime));
            MotorState accelerationState = new MotorState(motorSpeed / 2f, timeToChangeDirection);
            MotorState pingState = new MotorState(-motorSpeed, timeToChangeDirection);
            MotorState pongState = new MotorState(motorSpeed, timeToChangeDirection);
            randomState.nextState = accelerationState;
            accelerationState.nextState = pingState;
            pingState.nextState = pongState;
            pongState.nextState = pingState;

            SetCurrentState(randomState);
        }

        private void SetCurrentState(MotorState value)
        {
            currentState = value;
            timer = currentState.timeToChangeDirection;
        }

        public void Update(float deltaTime)
        {
            timer -= deltaTime;
            if (timer <= 0f) SetCurrentState(currentState.nextState);
        }

        public JointMotor2D GetJointMotor2D()
        {
            return currentState.motor2D;
        }
    }
}
