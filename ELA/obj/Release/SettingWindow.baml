   M S B A M L   `   `   `  ���� J  FWpfApplication1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:WpfApplication1WpfApplication1  #    WpfApplication1.SettingWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     /localclr-namespace:WpfApplication1       � Icon$+  %pack://siteoforigin:,,,/Icon/gear.ico�� � 
ResizeMode$ NoResize=� � Title$ ELA - Parameter Setting��$	��426��$	��315��.��� $��	0,0,-6,-3q�#��� ��$	��401��$	��Top=�.+��� -    ��Name$ label_Quality��$��Quality:��$��	10,10,0,0q�$
��Bold"�$��12(�$
��Left=�$	��115��$��26��$	��Top=��� -   $ label_Threshold��$��
Threshold:��$��	10,73,0,0q�$
��Bold"�$��12(�$��26��$	��Top=�$
��Left=�$	��115���� -   $ label_Blocksize��$��
Blocksize:��$��
10,134,0,0q�$
��Bold"�$��12(�$
��Left=�$	��115��$��26��$	��Top=��� -   $ label_Location��$��Location��$��
10,203,0,0q�$
��Bold"�$��12(�$
��Left=�$��62��$��26��$	��Top=��� -   $ label_Denoise��$��Denoise��$��
10,261,0,0q�$
��Bold"�$��12(�$
��Left=�$��59��$��26��$	��Top=��� -    ��Name$ Denoise_comboBox��$��
32,292,0,0q� �� SelectedIndex$ 0��$ �� IsSynchronizedWithCurrentItem$
 TrueY�$��12(�$��Normal"�$
��Left=�$	��230��$��22��$	��Top=�.@��� $��None - ColorScale ELA��$��Normal"�$��12(�$
��Left=��� $��None - GrayScale ELA��$��Normal"�$��12(�$
��Left=��� $��Square Mean Denoise��$��Normal"�$��12(�$
��Left=��� $��Median Filter Denoise��$��Normal"�$��12(�$
��Left=��� $��Gaussian Filter Denoise��$��Normal"�$��12(�$
��Left=��� -   $ Location_comboBox��$��
32,234,0,0q�$ 0��$��12(�$��Normal"�$
��Left=�$	��230��$��22��$	��Top=�.@��� $"��Tamper Region Location(Dots)��$��Normal"�$��12(�$
��Left=��� $%��Tamper Region Location(Windows)����  � Cursor$
 None��$��ELA Overlay Image��$��Normal"�$��12(��� $��
Test Image��$��Normal"�$��12(�$
��Left=��� -    ��Name$ quality_slider��$��	32,42,0,0q�	 � Maximum$		 100Q�
 � SmallChange$
 1Q� � LargeChange$ 1Q�$��26��$	��Top=�$
��Left=�$	��230���� -	   $ threshold_slider��$��
32,104,0,0q�$	 100Q�$
��Left=�$	��230��$��26��$	��Top=�$		 100Q�$
 1Q� � Minimum$ 10Q��� -
   $ blocksize_slider��$��
32,165,0,0q�$
 1Q�$ 20Q�$	 24Q�$
��Left=�$	��230��$��26��$	��Top=��� -   $ label��$��4��$��
10,165,0,0q�$��Normal"�$��12(�$��26��$	��Top=�$
��Left=�$��17���� -   $ label1��$��24��$��262,165,0,0q�$��Normal"�$��12(�$��26��$	��Top=�$
��Left=�$��24���� -   $ label2��$	��1.0��$��	5,104,0,0q�$��Normal"�$��12(�$
��Left=�$��27��$��26��$	��Top=��� -   $ label3��$
��10.0��$��257,104,0,0q�$��Normal"�$��12(�$
��Left=�$��34��$��26��$	��Top=��� -   $ label4��$��0��$��	10,42,0,0q�$��Normal"�$��12(�$
��Left=�$��17��$��26��$	��Top=��� -   $ label5��$	��100��$��
257,42,0,0q�$��Normal"�$��12(�$
��Left=�$��31��$��26��$	��Top=��� -    ��Name$ button_Confirm��$��Confirm��$
��Left=�$��
32,347,0,0q�$	��Top=�$��76���� -   $ button_Cancel��$��Cancel��$
��Left=�$��187,347,0,0q�$	��Top=�$��75��