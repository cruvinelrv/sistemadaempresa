package Core.Utils
{
	public final class MyArrayUtils
	{
		
		public static function cloneArrayEntidades(source:Array):Array
		{
			var ar:Array = [];
			for each(var o:* in source)
			{
				ar.push(o.clone());
			}
			return ar;
		}
		
		public static function asDictionary(source:Array, primaryKey:String="id"):Array
		{
			var ret:Array = [];
			var i:int=0;
			while(i<source.length)
			{
				var val:* = source[i];
				ret[ val[primaryKey] ] = val;
				i++;
			}
			return ret;
		}
		
		public static function getItemByField(ar:Array, fieldName:String, value:*):Object
		{
			var obj:Object=null;
			var i:int=0;
			while(i<ar.length)
			{
				if (ar[i][fieldName]==value)
				{
					return ar[i];
					break;
				}
				i++;
			}
			return obj;
		}
		/**
		 * Esse método só deve ser usado para campos numéricos
		 * 
		 * */
		public static function getItemByMaxField(ar:Array, fieldName:String):Object
		{
			var obj:Object=null;
			var i:int=0;
			var maior:Number=0;
			while(i<ar.length)
			{
				if (ar[i][fieldName] >= maior)
				{
					maior = ar[i][fieldName];
					obj = ar[i];
				}
				i++;
			}
			return obj;
		}
		
	}
}