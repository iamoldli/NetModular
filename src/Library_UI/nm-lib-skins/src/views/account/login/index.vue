<template>
  <nm-login :actions="actions"/>
</template>
<script>
import api from '@api/account'
export default {
  data() {
    return {
      actions: {
        getVerifyCode() {
          return new Promise(resolve => {
            resolve({ 'id': '8805ac775abc4801829ab6b3d39a1d4b', 'base64String': 'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAGQAAAAoCAYAAAAIeF9DAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAANUSURBVGhD7ZixcdwwEEXRi2ccugBXwEiZc4cugAU4dwsMlKgExUzVgxJWcC3Qu1gsCZLYBUDoRIwObwZzEu4+sNiPBXhn5kZVNEMqoxlSGc2QyvgYQ15gGG6NIj4+g745zaBs7p+xvUHNJJVrshMxyPy9JqwaqGPlEYMeiWpWj1VhzD/bDgY9kEmXr9QaETiiFnOYBzHo0pWl3BUHY5i9QV/EpEtWIVWFBhsTNIf5AgZlRz10mBxq/eg6MzDfSvQ3mH81Jqo/GDSBHu8qamfiL1t/fH6MMolpcIFAQJPry8E8TUX6aXgGPRjRvS16NgZbjAkyaRPxw8zTieopXf8yfzeo+qTIxp6CObOjEPOzTD/28YrQjBn78xWBlK4/Z/6oIbwzzgRj7wpXGWcXw5WRqmdj2BzemefnLzMjd37dEKgtOPJmAzskF3tpF+gt0xvoIbn9u+vIw5hfNhnmdDZL4x9Anze/agiXqt0h7tW2SID8BMX6ARaWo2foqHoG/Tvo152fahAdFbhD/0CjYyPHoPL4af5hGkGfNj8ML7MJwn1Su9z2j7O+nkNIvxx9E149/fFyD+Mnobd6Gus79UUuV6Qs/uP8SOxyh6EF4NP7csVAEBjT/u0b7RthCeiZkP6AeFytj766Xjou/EdP5emsOP74/CH9LoseQkAYCO8SMPlQFQvKgny9iHJ/cJV0w831BBATgm/RLu3gLKKqobahOP60+fcEMulQAsJgbHuCrSKh6LF+4wtSLvTxFfTnDcHLIZSQjTHF8efPj8CwMrY0oe1l5vdoA8KmIel5QaFYV9zRFLornCG63h0NkLWjnhIi6ckYeJgoiv/c/GpKuTR9IR5P3I/d+CoR0iO+XkP6DkL960UvIX0HoP71opUw5gYNX7fHWXr8+fMr6STsLsGg+K6AUfD/jUnKKKxfPh7Qy/AF7iU/qToYvkC9xUeqY8+yfls1YAzo0uPPn19J5Qr/9MEtcPTZfgmIIarX4J9OqOH3EvdGIvzTBTX8XuDeSGQbP1aN8nQWIGd+mEJGfIISwIAbZYgpzDHCp5lSxl3SR6Xt/mlkcde0NVPyuXvKmil5fEq6minpfFqqmilptDRVRjOkMpohldEMqYxmSGU0Q6pinv8DB8fkpdumfiUAAAAASUVORK5CYII=' })
          })
        },
        login(params) {
          return new Promise(resolve => {
            api.login(params)
            resolve()
          })
        }
      }
    }
  }
}
</script>
