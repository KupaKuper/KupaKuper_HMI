using KupaKuper_DeviceSever.Help;

using Microsoft.Extensions.DependencyInjection;

namespace WinFromFrame_KupaKuper
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            
            // 构建服务集合
            var services = new ServiceCollection();
            ConfigureServices(services);  //注册各种服务类

            // 构建服务提供者
            var serviceProvider = services.BuildServiceProvider();
            
            // 通过服务提供者获取Form1实例
            Application.Run(serviceProvider.GetRequiredService<MainForm>());
        }

        /// <summary>
        /// 在DI容器中注册所有的服务类型 
        /// </summary>
        /// <param name="services"></param>
        private static void ConfigureServices(ServiceCollection services)
        {
            services.AddMyServerServices();

            //注册 FormMain 类
            services.AddScoped<MainForm>();
            services.AddScoped<PageControl>();
        }
    }
}