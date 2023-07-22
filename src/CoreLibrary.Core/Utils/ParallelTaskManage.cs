namespace CoreLibrary.Core
{
    /// <summary>
    /// 并行任务
    /// </summary>
    public class ParallelTaskManage
    {
        private Dictionary<int, Task> _taskDic;
        private List<int>? _exceptionTaskIds;
        private int errorCode;
        public ParallelTaskManage()
        {
            _taskDic = new Dictionary<int, Task>();
            errorCode = -1;
        }
        #region 运行任务
        /// <summary>
        /// 运行任务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="task"></param>
        /// <returns></returns>
        public int RunTask<T>(Task<T> task)
        {
            return _taskDic.TryAdd(task.Id, task) == true ? task.Id : errorCode;
        }
        /// <summary>
        /// 运行任务
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public int RunTask(Task task)
        {
            return _taskDic.TryAdd(task.Id, task) == true ? task.Id : errorCode;
        }
        /// <summary>
        /// 运行任务
        /// </summary>
        /// <param name="taskFunc"></param>
        /// <returns></returns>
        public int RunTask(Func<Task> taskFunc)
        {
            var task = taskFunc();
            return _taskDic.TryAdd(task.Id, task) == true ? task.Id : errorCode;
        }
        /// <summary>
        /// 运行任务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="taskFunc"></param>
        /// <returns></returns>
        public int RunTask<T>(Func<Task<T>> taskFunc)
        {
            var task = taskFunc();
            return _taskDic.TryAdd(task.Id, task) == true ? task.Id : errorCode;
        }

        #endregion
        /// <summary>
        /// 获取任务结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public T? GetTaskResult<T>(int taskId)
        {
            if (_taskDic.TryGetValue(taskId, out var task) && (this._exceptionTaskIds == null || !this._exceptionTaskIds.Contains(taskId)))
            {
                return ((Task<T>)task).Result; // 返回任务的结果
            }
            //日志上报
            Console.WriteLine($"Task information was not obtained,the task id is: {taskId}");
            return default(T);
        }
        /// <summary>
        /// 并行等待所有任务
        /// </summary>
        /// <returns></returns>
        public async Task ParallelWaitAllTasks()
        {
            await ParallelWaitAllTasksWithExceptionTaskIds(_taskDic.Values.ToArray());
        }

        #region 私有方法
        private async Task ParallelWaitAllTasksWithExceptionTaskIds(Task[] tasks)
        {
            try
            {
                //异步等待所有任务完成
                await Task.WhenAll(tasks);
            }
            catch (Exception)
            {
                // 等待任意一个任务发生异常
                Task completedTask = await Task.WhenAny(tasks);
                // 找到发生异常的任务
                foreach (var task in tasks)
                {
                    if (task.Status == TaskStatus.Faulted)
                    {
                        if (this._exceptionTaskIds == null)
                        {
                            this._exceptionTaskIds = new List<int>();
                        }
                        this._exceptionTaskIds.Add(task.Id);
                        if (task.Exception != null)
                        {
                            //日志上报                            
                            Console.WriteLine($"An error occurred in task {task.Id}: {task.Exception?.InnerException?.Message}");
                        }
                    }
                }
            }
        }
        #endregion
    }
}
