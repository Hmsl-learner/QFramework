﻿using UnityEngine;

namespace QFramework
{
	/// <summary>
	/// 给内部使用的框架主体
	/// </summary>
	[MonoSingletonPath("QFramework/Core")]
	public class Core : MonoBehaviour, ISingleton, IFramework
	{
		/// <summary>
		/// 框架层的底层模块
		/// </summary>
		public IQFrameworkContainer FrameworkModuleContainer { get; private set; }

		/// <summary>
		/// 框架自带工具模块
		/// </summary>
		public IQFrameworkContainer UtilityContainer { get; private set; }
		
		private static IFramework mIntance
		{
			get { return MonoSingletonProperty<Core>.Instance; }
		}

		public static void RegisterUtility<TContract>(TContract utilityObject)
		{
			mIntance.UtilityContainer.RegisterInstance<TContract>(utilityObject);
		}
		
		public static void RegisterUtility<TContract, TImpl>() where TImpl : TContract, new()
		{
			mIntance.UtilityContainer.RegisterInstance<TContract>(new TImpl());
		}

		public static T GetUtility<T>() where T : class
		{
			return mIntance.UtilityContainer.Resolve<T>();
		}

		public void OnSingletonInit()
		{
			FrameworkModuleContainer = new QFrameworkContainer();
			
			UtilityContainer = new QFrameworkContainer();
			UtilityContainer.RegisterInstance<IJsonSerializeUtility>(new DefaultJsonSerializeUtility());
		}

		void Call()
		{
		}

		[RuntimeInitializeOnLoadMethod]
		static void InitOnLoad()
		{
			var framework = mIntance;
			(framework as Core).Call();
		}
	}
}