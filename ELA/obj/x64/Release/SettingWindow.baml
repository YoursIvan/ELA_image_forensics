   M S B A M L   `   `   `  ���� J  FWpfApplication1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:WpfApplication1WpfApplication1  #    WpfApplication1.SettingWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     /localclr-namespace:WpfApplication1       � Icon$&   pack://siteoforigin:,,,/gear.ico�� � 
ResizeMode$ NoResize=� � Title$ ELA - Parameter Setting��$	��426��$	��315��.��� $��0,0,2,0q�#��� ��$	��428��$	��Top=�.+��� -    ��Name$ label_Quality��$��Quality:��$��	18,10,0,0q�$
��Bold"�$��12(�$
��Left=�$	��115��$��26��$	��Top=��� -   $ label_Threshold��$��
Threshold:��$��	18,73,0,0q�$
��Bold"�$��12(�$��26��$	��Top=�$
��Left=�$	��115���� -   $ label_Blocksize��$��
Blocksize:��$��
18,134,0,0q�$
��Bold"�$��12(�$
��Left=�$	��115��$��26��$	��Top=��� -   $ label_Location��$��Location��$��
18,203,0,0q�$
��Bold"�$��12(�$
��Left=�$��62��$��26��$	��Top=��� -   $ label_Denoise��$��Denoise��$��
18,261,0,0q�$
��Bold"�$��12(�$
��Left=�$��59��$��26��$	��Top=��� -    ��Name$ Denoise_comboBox��$��
40,292,0,0q� �� SelectedIndex$ 0��$ �� IsSynchronizedWithCurrentItem$
 TrueY�$��12(�$��Normal"�$
��Left=�$	��230��$��22��$	��Top=�.@��� $��None - ColorScale ELA��$��Normal"�$��12(�$
��Left=��� $��None - GrayScale ELA��$��Normal"�$��12(�$
��Left=��� $��Square Mean Denoise��$��Normal"�$��12(�$
��Left=��� $��Median Filter Denoise��$��Normal"�$��12(�$
��Left=��� $��Gaussian Filter Denoise��$��Normal"�$��12(�$
��Left=��� $��Wavelet Threshold Denoise��$��Normal"�$��12(�$
��Left=��� -   $ Location_comboBox��$��
40,234,0,0q�$ 0��$��12(�$��Normal"�$
��Left=�$	��230��$��22��$	��Top=�.@��� $"��Tamper Region Location(Dots)��$��Normal"�$��12(�$
��Left=��� $%��Tamper Region Location(Windows)����  � Cursor$
 None��$��ELA Overlay Image��$��Normal"�$��12(��� $��Original Image��$��Normal"�$��12(�$
��Left=��� -    ��Name$ quality_slider��$��	40,42,0,0q�	 � Maximum$		 100Q�
 � SmallChange$
 1Q� � LargeChange$ 1Q�$��26��$	��Top=�$
��Left=�$	��230���� -	   $ threshold_slider��$��
40,104,0,0q�$	 100Q�$
��Left=�$	��230��$��26��$	��Top=�$		 100Q�$
 1Q� � Minimum$ 10Q��� -
   $ blocksize_slider��$��
40,165,0,0q�$
 1Q�$ 20Q�$	 24Q�$
��Left=�$	��230��$��26��$	��Top=��� -   $ label��$��4��$��
18,165,0,0q�$��Normal"�$��12(�$��26��$	��Top=�$
��Left=�$��17���� -   $ label1��$��24��$��270,165,0,0q�$��Normal"�$��12(�$��26��$	��Top=�$
��Left=�$��24���� -   $ label2��$	��1.0��$��
13,104,0,0q�$��Normal"�$��12(�$
��Left=�$��27��$��26��$	��Top=��� -   $ label3��$
��10.0��$��265,104,0,0q�$��Normal"�$��12(�$
��Left=�$��34��$��26��$	��Top=��� -   $ label4��$��0��$��	18,42,0,0q�$��Normal"�$��12(�$
��Left=�$��17��$��26��$	��Top=��� -   $ label5��$	��100��$��
265,42,0,0q�$��Normal"�$��12(�$
��Left=�$��31��$��26��$	��Top=��� -    ��Name$ button_Confirm��$��Confirm��$
��Left=�$��
40,347,0,0q�$	��Top=�$��76���� -   $ button_Cancel��$��Cancel��$
��Left=�$��195,347,0,0q�$	��Top=�$��75��